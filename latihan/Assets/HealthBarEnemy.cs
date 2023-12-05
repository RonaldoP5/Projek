using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private float _timeToDrain = 0.25f;
    [SerializeField] private Gradient _healthBarGradient;

    private Image _image;
    private float _target = 1f;

    private Color _newHealthBarColor;

    private Coroutine drainHealthBarCoroutine;
   
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

        _image.color = _healthBarGradient.Evaluate(_target);

        CheckHealthBarGradientAmount();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _target = currentHealth / maxHealth;

        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());

        CheckHealthBarGradientAmount();
    }

    private IEnumerator DrainHealthBar ()
    {
        float fillAmount = _image.fillAmount;
        Color currentColor = _image.color;

        float elapsedTime = 0f;
        while(elapsedTime < _timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            _image.fillAmount = Mathf.Lerp(fillAmount, _target, (elapsedTime / _timeToDrain));

            _image.color = Color.Lerp(currentColor, _newHealthBarColor, (elapsedTime / _timeToDrain));
            yield return null;
        }
    }

    private void CheckHealthBarGradientAmount()
    {
        _newHealthBarColor= _healthBarGradient.Evaluate(_target);

    }
}
