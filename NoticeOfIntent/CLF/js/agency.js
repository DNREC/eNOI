/* CLASSIE JS */
!function(s){"use strict";function e(s){return new RegExp("(^|\\s+)"+s+"(\\s+|$)")}function n(s,e){var n=a(s,e)?c:t;n(s,e)}var a,t,c;"classList"in document.documentElement?(a=function(s,e){return s.classList.contains(e)},t=function(s,e){s.classList.add(e)},c=function(s,e){s.classList.remove(e)}):(a=function(s,n){return e(n).test(s.className)},t=function(s,e){a(s,e)||(s.className=s.className+" "+e)},c=function(s,n){s.className=s.className.replace(e(n)," ")});var i={hasClass:a,addClass:t,removeClass:c,toggleClass:n,has:a,add:t,remove:c,toggle:n};"function"==typeof define&&define.amd?define(i):s.classie=i}(window);
/* END CLASSIE JS */

/* UI SEARCH JS */
!function(t){"use strict";function e(){var e,n=!1;return e=navigator.userAgent||navigator.vendor||t.opera,(/(android|ipad|playbook|silk|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(e)||/1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(e.substr(0,4)))&&(n=!0),n}function n(t,e){this.el=t,this.inputEl=t.querySelector("form > input.sb-search-input, input.sb-search-main-input"),this._initEvents()}!t.addEventListener&&t.Element&&function(){function t(t,e){Window.prototype[t]=HTMLDocument.prototype[t]=Element.prototype[t]=e}var e=[];t("addEventListener",function(t,n){var i=this;e.unshift({__listener:function(t){t.currentTarget=i,t.pageX=t.clientX+document.documentElement.scrollLeft,t.pageY=t.clientY+document.documentElement.scrollTop,t.preventDefault=function(){t.returnValue=!1},t.relatedTarget=t.fromElement||null,t.stopPropagation=function(){t.cancelBubble=!0},t.relatedTarget=t.fromElement||null,t.target=t.srcElement||i,t.timeStamp=+new Date,n.call(i,t)},listener:n,target:i,type:t}),this.attachEvent("on"+t,e[0].__listener)}),t("removeEventListener",function(t,n){for(var i=0,o=e.length;i<o;++i)if(e[i].target==this&&e[i].type==t&&e[i].listener==n)return this.detachEvent("on"+t,e.splice(i,1)[0].__listener)}),t("dispatchEvent",function(t){try{return this.fireEvent("on"+t.type,t)}catch(o){for(var n=0,i=e.length;n<i;++n)e[n].target==this&&e[n].type==t.type&&e[n].call(this,t)}})}(),!String.prototype.trim&&(String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g,"")}),n.prototype={_initEvents:function(){var t=this,e=function(e){e.stopPropagation(),t.inputEl.value=t.inputEl.value.trim(),classie.has(t.el,"sb-search-open")?classie.has(t.el,"sb-search-open")&&/^\s*$/.test(t.inputEl.value)&&(e.preventDefault(),t.close()):(e.preventDefault(),t.open())};this.el.addEventListener("click",e),this.el.addEventListener("touchstart",e),this.inputEl.addEventListener("click",function(t){t.stopPropagation()}),this.inputEl.addEventListener("touchstart",function(t){t.stopPropagation()})},open:function(){var t=this;classie.add(this.el,"sb-search-open"),e()||this.inputEl.focus();var n=function(i){e()||(this.removeEventListener("click",n),this.removeEventListener("touchstart",n),t.close())};document.addEventListener("click",n),document.addEventListener("touchstart",n)},close:function(){this.inputEl.blur(),classie.remove(this.el,"sb-search-open")}},t.UISearch=n}(window);
/* END UI SEARCH JS */


(function($) { // Use Dollar Signs
// Carousel Auto-Cycle
  $(document).ready(function() {
    $('.carousel').carousel({
      interval: 6000
    });

    //TOGGLE FONT AWESOME ON CLICK
    jQuery('.intToggle').click(function(){
        jQuery(this).find("svg.svg-inline--fa").toggleClass("fa-caret-right fa-caret-down");
    });


    // Swap alignment at mobile
    var $window = $(window);
        // Function to handle changes to style classes based on window width
        //Different from Bootstrap's 991px but working and tested
        function checkWidth() {
        if ($window.width() < 977) {
            $('.building-icon').removeClass('noLeftPad').addClass('text-center');
            $('.building-text').addClass('text-center');
            $('.weatherImg').removeClass('alignleft').addClass('text-center');
            $('.airQuality').addClass('text-center');
            $('.icon-mobile').addClass('text-center');
            $('.dnrec-highlights').addClass('text-center');
            $('.highlights-img').addClass('center-block');
            $('#collapseDNRECfinance').collapse('hide');
            $('#collapseDNRECcommunity').collapse('hide');
            $('#collapseDNRECclimate').collapse('hide');
            $('#collapseDNRECcoastal').collapse('hide');
            $('#collapsefishwildlife').collapse('hide');
            $('#collapseDNRECwaste').collapse('hide');
            $('#collapseDNRECwater').collapse('hide');
            $('#collapseDNRECair').collapse('hide');
            $('#collapseDNRECparks').collapse('hide');
            $('#collapseDNRECwatershed').collapse('hide');
            $('#collapseDNREClaw').collapse('hide');

        };
        if ($window.width() >= 977) {
            $('.building-icon').removeClass('text-center').addClass('noLeftPad');
            $('.building-text').removeClass('text-center');
            $('.weatherImg').removeClass('text-center').addClass('alignleft');
            $('.airQuality').removeClass('text-center');
            $('.icon-mobile').removeClass('text-center');
            $('.dnrec-highlights').removeClass('text-center');
            $('.highlights-img').removeClass('center-block');
            $('#collapseDNRECfinance').collapse('show');
            $('#collapseDNRECcommunity').collapse('show');
            $('#collapseDNRECclimate').collapse('show');
            $('#collapseDNRECcoastal').collapse('show');
            $('#collapsefishwildlife').collapse('show');
            $('#collapseDNRECwaste').collapse('show');
            $('#collapseDNRECwater').collapse('show');
            $('#collapseDNRECair').collapse('show');
            $('#collapseDNRECparks').collapse('show');
            $('#collapseDNRECwatershed').collapse('show');
            $('#collapseDNREClaw').collapse('show');

        }
    }
    // Execute on load
    checkWidth();
    // Bind event listener
    $(window).resize(checkWidth);

      //Smooth Scroll
      $('a[class="anchors_link"]').each(function() {
      if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'')
      && location.hostname == this.hostname
      && this.hash.replace(/#/,'') ) {
        var $targetId = $(this.hash), $targetAnchor = $('[id=' + this.hash.slice(1) +']');
        var $target = $targetId.length ? $targetId : $targetAnchor.length ? $targetAnchor : false;
         if ($target) {
           var targetOffset = $target.offset().top;
           $(this).click(function() {
             $('html, body').animate({scrollTop: targetOffset}, 500);
             return false;
              });
            }
          }
       });

}); // End Ready

})(jQuery); // End Dollar Signs

/* BEGIN READY FUNCTIONS */

jQuery(document).ready(function() {

    var jQuerylogo = jQuery('.logo_fade');

    var jQueryfade = jQuery('.delaware_fade');

    var jQuerysearchico = jQuery('#sb-search');

    var jQuerymenutext = jQuery('.menu_text');

    // Fade in the Delaware D on scroll
    jQuery(document).scroll(function() {
      jQuerylogo.css({display: jQuery(this).scrollTop()>60 ? "block":"none"});
      jQueryfade.css({display: jQuery(this).scrollTop()>60 ? "block":"none"});
      jQuerysearchico.css({display: jQuery(this).scrollTop()>60 ? "block":"none"});
      jQuerymenutext.css({display: jQuery(this).scrollTop()>60 ? "none":"block"});
    });

    // Fade in the Delaware Text on scroll at mobile
    jQuery(window).scroll(function(){
      var scroll = jQuery(window).scrollTop();
      jQuery(".logo_fade, .delaware_fade").css("opacity", 1.0 - 60 / jQuery(window).scrollTop());
    });

    // Footer + to - sign
    jQuery('.toggle-footer-btn').click(function() {
        jQuery(this).html(jQuery(this).html() );
    jQuery('#footer').slideToggle(400)
        return false;
    });

    //TOGGLE FONT AWESOME ON CLICK
    jQuery('.faq-links').click(function(){
        var collapsed=jQuery(this).find('i').hasClass('fa-question-circle');
    jQuery('.faq-links').find('i').removeClass('fa-check-circle');
    jQuery('.faq-links').find('i').addClass('fa-question-circle');
        if(collapsed)
    jQuery(this).find('i').toggleClass('fa-question-circle fa-2x fa-check-circle fa-2x')
    });
    jQuery('.faq-links').click(function(){
    var collapsed=jQuery(this).find('span').hasClass('text-danger');
    jQuery('.faq-links').find('span').removeClass('text-success');
    jQuery('.faq-links').find('span').addClass('text-danger');
    if(collapsed)
    jQuery(this).find('span').toggleClass('text-danger text-success')
    });

    // Reset Font Size
    var originalFontSize = jQuery('body, p, container, ul, li, ol').css('font-size');
    jQuery('.resetFont').click(function(){
    jQuery('body, p, container, ul, li, ol').css('font-size', originalFontSize);
    return false;
    });

    // Increase Font Size
    jQuery('.increaseFont').click(function(){
        var currentFontSize = jQuery('body, p, container, ul, li, ol').css('font-size');
        var currentFontSizeNum = parseFloat(currentFontSize, 10);
        var newFontSize = currentFontSizeNum*1.2;
    jQuery('body, p, container, ul, li, ol').css('font-size', newFontSize);
        return false;
    });

    // Decrease Font Size
    jQuery('.decreaseFont').click(function(){
        var currentFontSize = jQuery('body, p, container, ul, li, ol').css('font-size');
        var currentFontSizeNum = parseFloat(currentFontSize, 10);
        var newFontSize = currentFontSizeNum*0.8;
    jQuery('body, p, container, ul, li, ol').css('font-size', newFontSize);
        return false;
    });

    // Makes the Agency Nav float at the top of the page
    jQuery('#cssmenuTop').affix({
      offset: {
        top: '60'
      }
    });

    /* SEARCH FUNCTIONS */
    jQuery(".sb-search-main-submit").click(function() {
      var delawareGlobalSearchText = document.getElementById("txtKeywordMain").value;
      if(delawareGlobalSearchText == '') {
      // Do Nothing
      } else {
		  ga('send', 'pageview', 'https://dnrec.alpha.delaware.gov?parameter='+delawareGlobalSearchText);
          window.location = "/?query="+delawareGlobalSearchText;
      }
    return false;
  });

  jQuery(".sb-search-submit").click(function() {
      var delawareGlobalSearchText = document.getElementById("txtKeyword").value;
      if(delawareGlobalSearchText == '') {
      // Do Nothing
      } else {
		  ga('send', 'pageview', 'https://dnrec.alpha.delaware.gov?parameter='+delawareGlobalSearchText);
          window.location = "/?query="+delawareGlobalSearchText;
      }
    return false;
  });
  /* END SEARCH */


  	jQuery("#txtKeywordMain").autocompletion({
  	  datasets: {
  		   organic: {
  			   collection: funnelbackCollectionAutocomplete,
  			   profile: funnelbackProfile,
  			   program: funnelbackUrl + '/s/suggest.json',
  			   format: 'extended',
  			   alpha: '0.5',
  			   show: '10',
  			   sort: '0',
  			   group: true
  		   },
  	  },
  	   typeahead: {
  		   hint: true,
  		   events: {
  			   select: function (event, suggestion) {
  				   jQuery(".sb-search-main-submit").trigger("click");
  				   event.preventDefault(); // Cancel the native event
  				   event.stopPropagation();
  			   }
  		   }
  	   },
  	   length: 3
  	});

  	jQuery("#txtKeyword").autocompletion({
  	datasets: {
  	   organic: {
  		   collection: funnelbackCollectionAutocomplete,
  		   profile: funnelbackProfile,
  		   program: funnelbackUrl + '/s/suggest.json',
  		   format: 'extended',
  		   alpha: '0.5',
  		   show: '10',
  		   sort: '0',
  		   group: true
  	   },
  	},
  	typeahead: {
  	   hint: true,
  	   events: {
  		   select: function (event, suggestion) {
  			   jQuery(".sb-search-submit").trigger("click");
  			   event.preventDefault(); // Cancel the native event
  			   event.stopPropagation();
  		   }
  	   }
  	},
  	length: 3
	});

});

// Creates Agency Menu
(function(jQuery) {

  jQuery.fn.menumaker = function(options) {

      var cssmenu = jQuery(this), settings = jQuery.extend({
        title: "Menu",
        format: "dropdown",
        sticky: false
      }, options);

      return this.each(function() {

        cssmenu.prepend('<div id="menu-button">' + settings.title + '<div id="nav-icon-hamburger"><span></span><span></span><span></span><span></span><span></span><span></span></div></div>');

        jQuery(this).find("#menu-button").on('click', function(){
          jQuery(this).toggleClass('menu-opened');
          var mainmenu = jQuery(this).next('ul');
          if (mainmenu.hasClass('open')) {
            mainmenu.hide().removeClass('open');
          }
          else {
            mainmenu.show().addClass('open');
            if (settings.format === "dropdown") {
              mainmenu.find('ul').show();
            }
          }
        });

        jQuery(this).find("#nav-icon-hamburger").on('click', function(){
          jQuery(this).toggleClass('menu-opened');
          var mainmenu = jQuery(this).next('ul');
          if (mainmenu.hasClass('open')) {
            mainmenu.hide().removeClass('open');
          }
          else {
            mainmenu.show().addClass('open');
            if (settings.format === "dropdown") {
              mainmenu.find('ul').show();
            }
          }
        });

        jQuery("#menu-button").click(function() {
            var jQueryclicked = jQuery(this);
            jQuery("#agencyitems").each(function(index) {
                if (!jQuery(this).is(jQueryclicked))
                {
                    jQuery(this).toggle();
                }
            });
        });

        cssmenu.find('li ul').parent().addClass('has-sub');

        multiTg = function() {
          cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
          cssmenu.find('.submenu-button').on('click', function() {
            jQuery(this).toggleClass('submenu-opened');
            if (jQuery(this).siblings('ul').hasClass('open')) {
              jQuery(this).siblings('ul').removeClass('open').hide();
            }
            else {
              jQuery(this).siblings('ul').addClass('open').show();
            }
          });
        };

        if (settings.format === 'multitoggle') multiTg();
        else cssmenu.addClass('dropdown');

if (settings.sticky === true) cssmenu.css('position', 'fixed');

/**
* Store the window width */
var windowWidth = jQuery(window).width();
        resizeFix = function() {
/**
* Check window width has actually changed and it's not just iOS triggering a resize event on scroll */

if (jQuery(window).width() != windowWidth) {
/**
* Update the window width for next time */

windowWidth = jQuery(window).width();
setTimeout(function(){
 if (jQuery( window ).width() > 874) {
cssmenu.find('ul').show();
 }
 if (jQuery(window).width() <= 874) {
cssmenu.find('ul').hide().removeClass('open');
jQuery('.submenu-button').removeClass('submenu-opened');
jQuery('.submenu-button').addClass('submenu-closed');
 }
}, 500);

}
        };
        resizeFix();
        return jQuery(window).on('resize', resizeFix);
      });
  };

})(jQuery);

// Creates Agency Menu Hamburger Button
(function(jQuery){

jQuery(document).ready(function() {
  jQuery("#cssmenu").menumaker({
    title: "&nbsp;",
    format: "multitoggle"
  });

  jQuery("#cssmenu").prepend("<div id='menu-line'></div>");

  var foundActive = false, activeElement, linePosition = 0, menuLine = jQuery("#cssmenu #menu-line"), lineWidth, defaultPosition, defaultWidth;

  jQuery("#cssmenu > ul > li").each(function() {
    if (jQuery(this).hasClass('active')) {
      activeElement = jQuery(this);
      foundActive = true;
    }
  });

  if (foundActive === false) {
    activeElement = jQuery("#cssmenu > ul > li").first();
  }

  defaultWidth = lineWidth = activeElement.width();

  defaultPosition = linePosition = activeElement.position().left;

  menuLine.css("width", lineWidth);
  menuLine.css("left", linePosition);

  jQuery("#cssmenu > ul > li").hover(function() {
    activeElement = jQuery(this);
    lineWidth = activeElement.width();
    linePosition = activeElement.position().left;
    menuLine.css("width", lineWidth);
    menuLine.css("left", linePosition);
  },
  function() {
    menuLine.css("left", defaultPosition);
    menuLine.css("width", defaultWidth);
  });

  });
})(jQuery);

jQuery(document).ready(function() {
    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/i)) {
        jQuery('#agency-search').hide();
    };

});

jQuery(document).ready(function() {
//Hide Fontawesome icons from linked images
    var links = document.querySelectorAll('a[href$=".pdf"]');
    for (var i = 0; i < links.length; i++) {
        if (links[i].children.length == 0) {
        links[i].classList.add('icon');
        }
    }
});

//Readspeaker Fix
window.rsConf = {general: {usePost: true}};
// end Readspeaker fix
