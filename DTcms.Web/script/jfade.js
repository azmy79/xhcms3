$(function($) {
	$.fn.jfade = function(settings) {
   
	var defaults = {
		start_opacity: "1",
		high_opacity: "1",
		low_opacity: ".5",
		timing: "500"
	};
	var settings = $.extend(defaults, settings);
	settings.element = $(this);
	$(settings.element).css("opacity",settings.start_opacity);
	$(settings.element).hover(
		function () {												  
			$(this).siblings().stop().animate({opacity: settings.low_opacity}, settings.timing);
		},
		function () {
			$(this).siblings().stop().animate({opacity: settings.start_opacity}, settings.timing);
		}
	);
	return this;
	}
	
})