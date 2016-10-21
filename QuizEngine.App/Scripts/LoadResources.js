// Try to get language info from the browser.
$.holdReady(true);
var lang = window.navigator.userLanguage || window.navigator.language;
if (lang) {
    if (lang.length > 2) {
        // Convert e.g. 'en-GB' to 'en'. We do not support
        // resources for specific cultures at the moment.
        lang = lang.substring(0, 2);
    }
    // Include the languages that we want to support
    // in the following condition.
    // If we do not support the current navigator language,
    // default to english.
    if (lang != 'en' && lang != 'it') {
        lang = 'en';
    }
}
else {
    // Default to english if we did not succeed in getting
    // a language from the browser.
    lang = 'en';
}
// Construct a language-specific resource path.
var resourcePath = '../Scripts/Resources.' + lang + '.js';
// Get a reference to the HEAD element of the HTML page.
var head = document.head || document.getElementsByTagName('head')[0];
// Dynamically add the resourcePath to the HEAD element
// to start loading the resources.
var scriptEl = document.createElement('script');
scriptEl.type = 'text/javascript';
scriptEl.src = resourcePath;
//Registers an event that will be triggered when the resources script have been parsed
head.appendChild(scriptEl);
$('head').on('registerLoadResources', function () {
    $('head').trigger('loadResources');
});