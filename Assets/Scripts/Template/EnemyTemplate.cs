using SL;
using UnityEngine;

public abstract class EnemyTemplate : MonoBehaviour, IClickable
{
    [Header("components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private GameObject[] pointsToMove;
    [SerializeField] protected string id;

    [Header("config")]
    [SerializeField] private Color colorOfEnemy = Color.white;
    [SerializeField] private float maxDistanceToMove = .1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private int pointsToPlayer = 1;

    private GameObject _currentPointToMove;
    private int _indexOfCurrentPointToMove = 0;
    private bool _canMove;
    private float timeToDead;
    
    private TeaTime _born, _run, _shot, _die;
    private bool _collisionTouch;
    private bool _isAlive = true;


    public string Id => id;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    public void Config(GameObject[] listOfPointsToMove)
    {
        ConfigStatePattern();
        pointsToMove = listOfPointsToMove;
        timeToDead = 1;
        _currentPointToMove = pointsToMove[_indexOfCurrentPointToMove];
        transform.position = _currentPointToMove.transform.position;
    }

    private void ConfigStatePattern()
    {
        _born = this.tt().Pause().Add(() =>
        {
            spriteRenderer.color = colorOfEnemy;
        }).Add(() =>
        {
            _run.Play();
        });
        
        _run = this.tt().Pause().Add(() =>
        {
            _canMove = true;
        }).Wait(()=> _collisionTouch, .1f).Add(() =>
        {
            if (!_isAlive)
            {
                _shot.Play();
            }
            else
            {
                //choco contra el player
                ServiceLocator.Instance.GetService<ILifeOfPlayer>().GetDamage(damage);
                _die.Play();
            }
        });
        
        _shot = this.tt().Pause().Add(() =>
        {
            //Debug.Log("shot");
            //VisualEffect or Sfx
        }).Add(() =>
        {
            _die.Play();
        });
        
        _die = this.tt().Pause().Add(() =>
        {
            //Debug.Log("die");
            Destroy(gameObject,timeToDead);
        });

        _born.Play();
    }

    private void Update()
    {
        if (!_canMove) return;
        //move to currentPointToMove
        var position = _currentPointToMove.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        //rotate to currentPointToMove
        var direction = position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //check if arrived with diference of distance
        if (Vector3.Distance(transform.position, _currentPointToMove.transform.position) < maxDistanceToMove)
        {
            //change currentPointToMove
            _indexOfCurrentPointToMove++;
            if (_indexOfCurrentPointToMove >= pointsToMove.Length)
            {
                _canMove = false;
            }
            else
            {
                _currentPointToMove = pointsToMove[_indexOfCurrentPointToMove];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Touch();
        }
    }

    public void Touch()
    {
        _collisionTouch = true;
        timeToDead = 0.5f;
    }
    
    public void Shot()
    {
        _isAlive = false;
        Touch();
        timeToDead = 0;
        ServiceLocator.Instance.GetService<IPointsToPlayer>().UpdatePoints(pointsToPlayer);
        collider2D.enabled = false;
    }
}