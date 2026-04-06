using UnityEngine;
using TMPro;


public class UIGame : MonoBehaviour
{
    public static UIGame Instance;

    [SerializeField] private TextMeshProUGUI _TxtTime;
    [SerializeField] private TextMeshProUGUI _TxtCollisions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("2 fois");
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        GestionCollision.OnCollisionOccured += GestionCollision_OnCollisionOccured;
    }

    private void OnDestroy()
    {
        GestionCollision.OnCollisionOccured -= GestionCollision_OnCollisionOccured;
    }
    private void Update()
    {
        float ElapsedTime = Time.time - GestionJeu.Instance.StartTime;
        _TxtTime.text = $"Temps: {ElapsedTime:f2}"; 
    }

    private void GestionCollision_OnCollisionOccured(object sender, GestionCollision.OnCollisionOccuredEventArgs e)
    {
        _TxtCollisions.text = $"Collisions : {GestionJeu.Instance.Pointage}";
    }
}
