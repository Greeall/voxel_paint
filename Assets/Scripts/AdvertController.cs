using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdvertController : MonoBehaviour {


	void Start()
	{
		 Advertisement.Initialize("2908049", true);
	}
	public void ShowRewardedAd()
	{
		#if UNITY_ADS
		if (Advertisement.IsReady())
		{

			ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
		else
		{
			Debug.Log("kuku");
		}
		#endif
	}
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
        	 case ShowResult.Finished:
                  //  Debug.Log("The ad was successfully shown.");
                    CoinsController.I.coins += 2;
					CoinsController.I.adverisingOffer.SetActive(false);
                    break;
            case ShowResult.Skipped:
                 //   Debug.Log("The ad was skipped before reaching the end.");
                    break;
            case ShowResult.Failed:
                  //  Debug.LogError("The ad failed to be shown.");
                    break;
        }
    }


   
    
}
