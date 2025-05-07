using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PetController : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider;

    [Header("Details")]
    public string petname;

    //hervivore or carnivor
    public string jenisHaiwan;

    public string jenisDarah;

    public string namaDiberi;

    public AudioSource audiosfx;

    [Header("References")]
    public Image picture;
    public Animator details;

    public Button btnConfirmChange;
    public Transform tetapanMenu;

    public Button AudioButton;

    void Awake()
    {
        picture = GameObject.Find("picture").GetComponent<Image>();
        details = GameObject.Find("Details").GetComponent<Animator>();
        btnConfirmChange = GameObject.Find("btnConfirm").GetComponent<Button>();
        AudioButton = GameObject.Find("AudioSfx").GetComponent<Button>();
        tetapanMenu = GameObject.Find("TetapanMenu").GetComponent<Transform>();
    }

    public void PlaySound(AudioSource audio)
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        tetapanMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Touch();

        if (
            tetapanMenu
                .transform.GetChild(1)
                .transform.GetChild(2)
                .GetComponent<TMP_InputField>()
                .text.Equals("")
        )
        {
            btnConfirmChange.interactable = false;
        }
        else
        {
            btnConfirmChange.interactable = true;
        }
    }

    public void WriteName()
    {
        string name = tetapanMenu
            .transform.GetChild(1)
            .transform.GetChild(2)
            .GetComponent<TMP_InputField>()
            .text;
        Debug.Log(name);
        InDetails.instance.textList[0].color = new Color32(12, 74, 7, 255);
        InDetails.instance.textList[0].text = name;
        PlayerPrefs.SetString(petname + "_" + PlayerPrefs.GetInt("CurrentPlayerNo_"), name);

        tetapanMenu
            .transform.GetChild(1)
            .transform.GetChild(2)
            .GetComponent<TMP_InputField>()
            .text = "";
        tetapanMenu.gameObject.SetActive(false);
    }

    private void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(
                Camera.main.ScreenPointToRay(Input.mousePosition)
            );
            if (hit)
            {
                if (hit.collider == collider)
                {
                    PetsManager.instance.pet.Add(hit.transform.GetComponent<PetController>());
                    PetsManager
                        .instance.pet[PetsManager.instance.pet.Count - 1]
                        .transform.GetChild(0)
                        .transform.gameObject.SetActive(true);
                    Debug.Log(hit.collider.name);
                    btnConfirmChange.onClick.RemoveAllListeners();
                    AudioButton.onClick.RemoveAllListeners();
                    //Do your thing.
                    Debug.Log("This is a = " + petname);
                    details.SetBool("istrue", false);
                    picture.sprite = GetComponent<SpriteRenderer>().sprite;
                    //InDetails.instance.textList[0].text = petname;
                    //InDetails.instance.textList[1].text = jenisHaiwan;
                    //InDetails.instance.textList[2].text = jenisDarah;
                    if (PlayerPrefs.GetString(petname + "_" + PlayerPrefs.GetInt("CurrentPlayerNo_")).Equals(""))
                    {
                        InDetails.instance.textList[0].color = new Color32(255, 0, 0, 255);
                        InDetails.instance.textList[0].text = "Belum Diberi";
                    }
                    else
                    {
                        InDetails.instance.textList[0].color = new Color32(12, 74, 7, 255);
                        InDetails.instance.textList[0].text = PlayerPrefs.GetString(petname + "_" + PlayerPrefs.GetInt("CurrentPlayerNo_"));
                    }

                    btnConfirmChange.onClick.AddListener(() => WriteName());
                    AudioButton.onClick.AddListener(
                        () =>
                            PlaySound(
                                hit.collider.transform.parent.GetChild(1)
                                    .GetComponent<AudioSource>()
                            )
                    );
                }
            }
            else
            {
                for (int i = 0; i < PetsManager.instance.pet.Count; i++)
                {
                    PetsManager
                        .instance.pet[i]
                        .transform.GetChild(0)
                        .transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
