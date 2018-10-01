jQuery(document).ready(function (){
	$('#onepage-nav').onePageNav();
	
	$(window).bind('scroll', function() {
         if ($(window).scrollTop() > 58) {
             $('#container-nav-div').addClass('navbar-fixed-top');
         }
         else {
             $('#container-nav-div').removeClass('navbar-fixed-top');
         }
    });
});