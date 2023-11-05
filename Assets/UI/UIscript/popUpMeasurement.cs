using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;


public class popUpMeasurement : MonoBehaviour
{

    public GameObject popup;
    public GameObject tagname;
    public GameObject measurement1;
    public GameObject measurement2;
    public GameObject measurement3;
    public GameObject measurement4;
    public string phpURL;
    
    TextMeshProUGUI TagnameText;
    TextMeshProUGUI measurementText1;
    TextMeshProUGUI measurementText2;
    TextMeshProUGUI measurementText3;
    TextMeshProUGUI measurementText4;
    private string url;

    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
        TagnameText = tagname.GetComponent<TextMeshProUGUI>();
        measurementText1 = measurement1.GetComponent<TextMeshProUGUI>();
        measurementText2 = measurement2.GetComponent<TextMeshProUGUI>();
        measurementText3 = measurement3.GetComponent<TextMeshProUGUI>();
        measurementText4 = measurement4.GetComponent<TextMeshProUGUI>();
        url = phpURL + "?name=" + name + "&amount=1";
    }

    void OnTriggerEnter(Collider other)
    {
        popup.SetActive(true);
        TagnameText.text = gameObject.name;
        string measurements1 = GetMeasurementFromDatabase();
        measurementText1.text = GetMeasurementByIndex(measurements1, 1);
        string measurements2 = GetMeasurementFromDatabase();
        measurementText2.text = GetMeasurementByIndex(measurements2, 2);
        string measurements3 = GetMeasurementFromDatabase();
        measurementText3.text = GetMeasurementByIndex(measurements3, 0);
        string measurements4 = GetMeasurementFromDatabase();
        measurementText4.text = GetMeasurementByIndex(measurements4, 0);
        
        if(measurementText1.text == "1" && measurementText2.text == "1"){
            measurementText4.text = "Generator på";
        }
        else if(measurementText1.text == "1" && measurementText2.text == "0"){
            measurementText4.text = "Generator starter";
        }
        else if(measurementText1.text == "0" && measurementText2.text == "0"){
            measurementText4.text = "Generator av";
        }
        else if(measurementText1.text == "0" && measurementText2.text == "1"){
            measurementText4.text = "FEIL";
        }

        if(measurementText1.text == "1"){
            measurementText1.text = "på";
        }
        else{
            measurementText1.text = "av";
        }
        if(measurementText2.text == "1"){
            measurementText2.text = "på";
        }
        else{
            measurementText2.text = "av";

        }
        
    }

    private string GetMeasurementByIndex(string measurement, int index)
    {
        string[] parts = measurement.Split(',');
        return parts[index];
    } 

    private string GetMeasurementFromDatabase()
    {
        string response;
        using (WebClient client = new WebClient())
        {
            response = client.DownloadString(url);
        }
        
        return response;    
    }

    void OnTriggerExit(Collider other)
    {
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
