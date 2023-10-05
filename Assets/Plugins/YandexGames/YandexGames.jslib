mergeInto(LibraryManager.library, {

  ShowRewardedAdExternal : function (id) {
    var success = false;
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: function() {
          console.log('Video ad open.');
          myGameInstance.SendMessage('YandexGamesDispatcher', 'OnRewardedAdOpen', id);
        },
        onRewarded: function() {
          console.log('Rewarded!');
          success = true;
        },
        onClose: function() {
          console.log('Video ad closed.');
          if (success) {
            myGameInstance.SendMessage('YandexGamesDispatcher', 'OnRewardedAdWatched', id);
          }
          myGameInstance.SendMessage('YandexGamesDispatcher', 'OnRewardedAdClosed', id);
        },
        onError: function(e) {
          console.log('Error while open video ad:', e);
          myGameInstance.SendMessage('YandexGamesDispatcher', 'OnRewardedAdError', id);
        }
      }
    });
  },

  GetLanguageExternal : function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  }
});