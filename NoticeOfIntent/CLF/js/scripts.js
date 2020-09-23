var $ = jQuery.noConflict();

//email Id validation
function validateEmail(email) {
  var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
}

//subscription page
function validate() {
  var $result = $("#validation_msg_email");
  var email = $("#st_email_add").val();
  $result.text("");
  if (email=== "") {
    $result.text(" Email Address is Required.");
    $result.css("color", "red");
  }else if (validateEmail(email)) {
    //email is valid 
    document.getElementById("st_add_subscribe_form").submit(); 
  } else {
    $result.text(" Email Address is Invalid.");
    $result.css("color", "red");
  }
  return false;
}

$("#st_subs_submit").bind("click", validate);


//edit subscription page
function validateEdit() {
  var $result = $("#validation_msg_email");
  var email = $("#unsubscribe_email_edit").val();
  $result.text("");
  if (email=== "") {
    $result.text(" Email Address is Required.");
    $result.css("color", "red");
  }else if (validateEmail(email)) {
    //email is valid 
    document.getElementById("wpp_unsubscribe").submit(); 
  } else {
    $result.text(" Email Address is Invalid.");
    $result.css("color", "red");
  }
  return false;
}

$("#unsubscribe_edit_submit").bind("click", validateEdit);

//email validation
jQuery( "#add-subscribe" ).click(function( event ) {
  
    event.preventDefault();
    var vali = true;
    var vali_email = true;
    var email = jQuery('#InputEmail1').val();
    var checkbox3 = jQuery('#inlineCheckbox3').prop("checked");

    //email validation
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (email.length === 0) {
      jQuery( '.email-error').text( "Email is mandatory" );
      jQuery( '.cbox-error').text( "Please select the subscriptions." );
      vali_email = false;
    }else if( !filter.test(email) ){
      jQuery( '.email-error' ).text("Invalid Email Address" );
      vali_email = false;
    }else{
      jQuery( '.email-error' ).text('');
      vali_email = true;
    }

    //checkbox validation
    if (checkbox3 == false  ) {
      jQuery( '.cbox-error').text( "Please select the subscriptions." );
      vali = false;
    }else{
      jQuery( '.cbox-error' ).text('');
      vali = true;
    }


    if (vali == true && vali_email == true) {
      jQuery( "#st_add_subscribe_form" ).submit();
    }

});

