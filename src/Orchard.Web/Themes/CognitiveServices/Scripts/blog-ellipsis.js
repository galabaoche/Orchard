(function ($) {
    wordEllipsis();
})(jQuery);

function wordEllipsis(){
    $(".blog-post-ellipsis").each(function(index){
        var width = $(this).parent("p").width() * 3;
		var readWidth = $(this).siblings(".blog-more").width();
        var text = $(this).text();
        var length = text.length;

        var $temp_elem = $(this).clone(false)
            .css({ "visibility": "hidden", "whiteSpace": "nowrap","width":"auto" })
            .appendTo($(this).parent());

        if($temp_elem.width() + readWidth > width) {
			while($temp_elem.width() > width && length > 0) {
				$temp_elem.text(text.substring(0, length-1));
				length--;
			}
		 
			$(this).text(text.substring(0, length - 18) + "…");
		}
        $temp_elem.remove();
    });
}