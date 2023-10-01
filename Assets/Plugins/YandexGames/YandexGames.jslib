mergeInto(LibraryManager.library, {

  ShowRewardedAdExternal : function (id) {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("YandexGamesDispatcher", "OnRewardedAdOpen", id);
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage("YandexGamesDispatcher", "OnRewardedAdWatched", id);
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("YandexGamesDispatcher", "OnRewardedAdClosed", id);
        },
        onError: (e) => {
          console.log('Error while open video ad:', e);
          myGameInstance.SendMessage("YandexGamesDispatcher", "OnRewardedAdError", id);
        }
      }
    })
  }

});