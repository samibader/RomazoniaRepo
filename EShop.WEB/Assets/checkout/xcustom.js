
$(document).ready(function(){$(".xpersonal").append($(".xpersonal xdiv").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$(".xpersonal1").append($(".xpersonal1 xdiv").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){
	
	
	$(".xaddress").show();
});
$(document).ready(function(){$( ".xaddress1").append($(".xaddress1 tr").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$( ".xaddress2").append($(".xaddress2 tr").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$( ".xaddress").append($(".xaddress xdiv").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$( ".xaddress1").append($(".xaddress1 xdiv").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$(".xpassword").append($(".xpassword tr").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
$(document).ready(function(){$(".xpassword").append($(".xpassword xdiv").get().sort(function(a, b) {
    return parseInt($(a).attr("sort").match(/\d+/), 10)
         - parseInt($(b).attr("sort").match(/\d+/), 10);
}));
});
/* Old with error on right side keyboard
 $(document).ready(function() {
	$(".numeric").keydown(function(event) {
		// Allow only backspace and delete
		if ( event.keyCode == 46 || event.keyCode == 9 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 8 || event.keyCode == 16 || event.keyCode == 36 || event.keyCode == 35) {
			// let it happen, don't do anything
		}
		else {
			// Ensure that it is a number and stop the keypress
			if (event.keyCode < 48 || event.keyCode > 57 ) {
				event.preventDefault();	
			}	
		}
	});
});
*/
$(document).ready(function() {
	$(".numeric").keydown(function(e) {		
		 // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) || 
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
	});
});
$(document).ready(function(){
    $( document ).on( 'focus', '.mask , .numeric', function(){
        $( this ).attr( 'autocomplete', 'off' );
    });
});