var allContentDirs="account currency download enlist events fansite-kit game-guide home legal media password-reset promotions redeemcode signin advancebattalion patchtest updates universe".split(" "),loggedOutOnly=["enlist","genesis","signin"],dirAlias=[];dirAlias.ascension="updates/ascension/";dirAlias.c2e2="events/c2e22013/";dirAlias.eccc="events/eccc2013/";dirAlias.eccc2013="events/eccc2013/";dirAlias.gdc="events/gdc2013/";dirAlias.gdc2013="events/gdc2013/";dirAlias.invasion="updates/invasion/";
dirAlias.paxeast="events/paxeast2013/";dirAlias.paxeast2013="events/paxeast2013/";dirAlias.sxsw="events/sxsw2013/";dirAlias.sxsw2013="events/sxsw2013/";dirAlias.wreckage="updates/wreckage/";dirAlias.frostburn="promotions/frostburn/";dirAlias["game-info"]="game-guide/game-info/";dirAlias.genesis="enlist/cmx_genesis/";var urlAlias=[];urlAlias.forum="community.playhawken.com";urlAlias.forums="community.playhawken.com";urlAlias.news="community.playhawken.com/news";
var standalonePages=["promotions/frostburn","promotions/khangsmechs"];$(function(){_ignoreHashChange=!1;window.location.hash?pageFromHash():(displayContent(),postLoad(window.location.pathname.split("/")[1]),initPageContent());$(window).bind("hashchange",function(){pageFromHash();_ignoreHashChange&&(_ignoreHashChange=!1)})});
function pageFromHash(){if(!1===_ignoreHashChange){var e=new $.Deferred;loadHashPage(e);e.done(function(d){document.body.style.cursor="default";postLoad(d);initPageContent()})}}
function loadHashPage(e){document.body.style.cursor="wait";var d=readCookie("phaccess");if(window.location.hash)b=location.hash.split("#")[1].split("?")[1],a=location.hash.split("#")[1].split("?")[0].split("/"),c=a[0];else var b="",a="",c="home";if("-1"!="hab advancebatalion advancebatallion advancebattallion advancedbatalion advancedbattalion advancedbatallion advancedbattallion advancedbuttstallion".split(" ").indexOf(c.toLowerCase()))return window.location.hash="#advancebattalion",!1;if(urlAlias[c.toLowerCase()])return window.location=
window.location.protocol+"//"+urlAlias[c.toLowerCase()],!1;"-1"!=loggedOutOnly.indexOf(c.toLowerCase())&&d?window.location.hash="#home":("-1"==allContentDirs.indexOf(c.toLowerCase())&&!dirAlias[c.toLowerCase()]?(c="errors",d="?errorID=404"):(dirAlias[c.toLowerCase()]&&(a=dirAlias[c.toLowerCase()].split("/"),c=a[0]),""==a[a.length-1]&&a.pop(),1<a.length?(a.shift(),d=b&&""!=b?"?"+getParamString(c,a)+"&"+b:"?"+getParamString(c,a)):d=b&&""!=b?"?"+b:""),d=""==d?d+"?ajax_pageRequest="+location.hash.split("#")[1]:
d+"&ajax_pageRequest="+location.hash.split("#")[1],d="/content/"+c.toLowerCase()+"/index.php"+d,0<$("link.css-dyn[content="+c+"]").length||(b="/content/"+c.toLowerCase()+"/css/"+c.toLowerCase()+".css?v= 21570",$("head").append($("<link>").attr({rel:"stylesheet",href:b,type:"text/css","class":"css-dyn",content:c}))),"-1"!=standalonePages.indexOf(c+"/"+a[0])?window.location="/"+location.hash.split("#")[1]:(0<$(document).scrollTop()&&($("div.page_body div, div.page_body ul, div.page_body iframe").addClass("scrollHide"),
$(document).scrollTop(0)),$("#content_display").find("#page").load(d,function(){$("link.css-dyn[content!="+c+"]").remove();displayContent();e.resolve(c)})))}
Array.prototype.indexOf||(Array.prototype.indexOf=function(e){if(null==this)throw new TypeError;var d=Object(this),b=d.length>>>0;if(0===b)return-1;var a=0;1<arguments.length&&(a=Number(arguments[1]),a!=a?a=0:0!=a&&(Infinity!=a&&-Infinity!=a)&&(a=(0<a||-1)*Math.floor(Math.abs(a))));if(a>=b)return-1;for(a=0<=a?a:Math.max(b-Math.abs(a),0);a<b;a++)if(a in d&&d[a]===e)return a;return-1});
var params_account,params_advancebattalion,params_currency,params_download,params_enlist,params_events,params_forums,params_game_guide,params_home,params_legal,params_media,params_news,params_password_reset,params_promotions,params_redeemcode,params_signin,params_universe,params_updates;
function getParamString(e,d){e=e.replace("-","_").toLowerCase();params_account=["callsign"];params_advancebattalion=["document"];params_currency=["Key","Store"];params_download=["token","guid"];params_enlist=["token","ref"];params_events=["event"];params_forums=["forumParams"];params_game_guide=["topic","subtopic","subject"];params_home=["user","clan","time"];params_legal=["document"];params_media=["mediaType","entry"];params_news=["newsParams"];params_password_reset=["passwordToken"];params_promotions=
["promo"];params_redeemcode=["event"];params_signin=["action"];params_universe=["subject"];params_updates=["update"];for(var b=[],a=0;a<d.length;a++){var c=eval("params_"+e+"["+a+"]");b.push(c+"="+d[a])}"-1"!=b[0].indexOf("&amp;")?b=b[0].split("&amp;"):"-1"!=b[0].indexOf("&")&&(b=b[0].split("&"));return b.join("&")};