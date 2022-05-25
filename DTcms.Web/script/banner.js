(function() {
	var mainObj = $(".slide_screen ul.list"),
	    focusbtns = $(".slide_screen .libtn"),
		lidom = $(".slide_screen li"),
		hoverclass = "selected",
		count =  $("li",focusbtns).length,
		speed = 1000,
		distim =6000,
		cur=1;

	$("li",focusbtns).eq(cur-1).addClass(hoverclass);
	for(var i=0; i<$("li",mainObj).length; i++) {
		var li = $("li",mainObj).eq(i);
		$(".window",li).css({'top':0, 'left':0, 'position':'absolute'}).css('width', parseInt(li.width())*count);
	}
	initEvent();
	var runid;
	autoPlay();
	
	//自动滚动开始
	function autoPlay() { 
	  runid = setInterval(run, distim);
	}; 
	//自动滚动停止
	function autoStop() { 
	  window.clearInterval(runid); 
	};
	function initEvent() {
		//点击图片则停止动画鼠标事件绑定 
		$(".piece").hover(function(){
			  autoStop();
			},
			function(){
			  autoPlay();
		}); 
		
		//点击小导航		
		$("li",focusbtns).click(function(){
			 var clickcount = $("li",focusbtns).index(this)+1;
			 var step =  Math.abs(clickcount-cur);
			 if(clickcount==cur) return;
			 autoStop();
			 $("li",focusbtns).removeClass(hoverclass);
			 lidom.each(function() {
				var liwindowk = $(".window",this);
				var pieceItem = $(".piece",liwindowk);
				var runidk;
				if(cur<clickcount){
					runidk = liwindowk.animate({"left": -parseInt(pieceItem.width())*step}, speed,function(){
						  $(".piece", liwindowk).each(function(ind) {
							  if(ind<step) {
								  $(this).insertAfter($(".piece",liwindowk).last());
							  }
						  });
						  liwindowk.css({left:0});
					  });
				}else if(cur>clickcount){
					$(".piece", liwindowk).each(function(ind2) {
						if(ind2<step) {
						  $(".piece",liwindowk).eq(count-1).insertBefore($(".piece",liwindowk).first());
						  liwindowk.css({left:-parseInt(pieceItem.width())*(ind2+1)});
						}
					 });
					 runidk = liwindowk.animate({"left": 0}, speed);					
				}
			  });	
			 if(clickcount>count){cur=1;}else{cur = clickcount;}		  
			 $("li",focusbtns).eq(cur-1).addClass(hoverclass);
			 autoPlay();
		});
	}
	//执行自动滚动
	function run() {
		$("li",focusbtns).eq(cur-1).removeClass(hoverclass);
		lidom.each(function() {	
			var liwindow=$(".window",this);
			liwindow.stop(true,true).animate({"left": -parseInt($(".piece",liwindow).width())}, speed,function(){
				$(".piece",liwindow).first().insertAfter($(".piece", liwindow).last());
				liwindow.css({left:0});
			});
		});		
		if(cur>=count){cur=1;}else{cur++;}		
		$("li",focusbtns).eq(cur-1).addClass(hoverclass);		
	}
	
})();; 