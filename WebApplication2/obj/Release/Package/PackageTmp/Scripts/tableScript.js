$(function () {
	$("td[colspan=3]").find("ul").hide();
	$("table").click(function (event) {
		console.log(event.target.className);
		if (event.target.className == "clickable") {
			event.stopPropagation();
			var $target = $(event.target);
			if ($target.closest("td").attr("colspan") > 1) {
				$target.slideUp();
			}
			else {
				$target.closest("tr").next().find("ul").slideToggle();
			}
		}
	});
});