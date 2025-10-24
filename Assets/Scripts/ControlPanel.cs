using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [Header("Vertical")]
    public Button upButton;
    public Button downButton;    

    [Header("Horizontal")]
    public Slider horizontalSlider;
    public float sliderReturnSpeed = 3f;

    [Header("Forward")]
    public Button forwardButton;
    public Button backButton;

    [Header("DEBUG")]
    public bool SHOW_DEBUG = false;
    public TextMeshProUGUI debugVertical;
    public TextMeshProUGUI debugHorizontal;
    public TextMeshProUGUI debugForward;

    private float vertical = 0;
    private float horizontal = 0;
    private float forward = 0;

    private bool isSliderBeingDragged = false;

    // Public properties for external access
    public float Vertical => vertical;
    public float Horizontal => horizontal;
    public float Forward => forward;

    private void OnEnable()
    {
        upButton.onClick.AddListener(OnUpClick);
        downButton.onClick.AddListener(OnDownClick);

        horizontalSlider.onValueChanged.AddListener(OnHorizontalValueChanged);

        forwardButton.onClick.AddListener(OnForwardClick);
        backButton.onClick.AddListener(OnBackClick);

        debugVertical.gameObject.SetActive(SHOW_DEBUG);
        debugHorizontal.gameObject.SetActive(SHOW_DEBUG);
        debugForward.gameObject.SetActive(SHOW_DEBUG);
    }

    private void OnDisable()
    {
        upButton.onClick.RemoveListener(OnUpClick);
        downButton.onClick.RemoveListener(OnDownClick);

        horizontalSlider.onValueChanged.RemoveListener(OnHorizontalValueChanged);

        forwardButton.onClick.RemoveListener(OnForwardClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void Update()
    {
        if (SHOW_DEBUG)
        {
            debugVertical.text = vertical.ToString("0.00");
            debugHorizontal.text = horizontal.ToString("0.00");
            debugForward.text = forward.ToString("0.00");
        }

        // auto centre slider
        if (!isSliderBeingDragged && horizontalSlider.value != 0f)
        {
            float newValue = Mathf.MoveTowards(horizontalSlider.value, 0f, sliderReturnSpeed * Time.deltaTime);
            horizontalSlider.value = newValue;
        }
    }

    private void OnForwardClick()
    {
        forward = Mathf.Clamp(forward + 1f, -3f, 3f);
    }

    private void OnBackClick()
    {
        forward = Mathf.Clamp(forward - 1f, -3f, 3f);
    }    

    public void OnSliderPointerDown()
    {
        isSliderBeingDragged = true;
    }

    public void OnSliderPointerUp()
    {
        isSliderBeingDragged = false;
    }

    private void OnHorizontalValueChanged(float newValue)
    {
        horizontal = Mathf.Clamp(newValue, -1f, 1f);
    }

    private void OnUpClick()
    {
        vertical = Mathf.Clamp(vertical + 1f, -3f, 3f);
    }

    private void OnDownClick()
    {
        vertical = Mathf.Clamp(vertical - 1f, -3f, 3f);
    }    
}
