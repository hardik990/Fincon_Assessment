$(document).ready(function () {
	$(".loader").hide();
    $("#liDashBoard").removeClass("child-menu").addClass('child-menu');
    $("#liGeneral").removeClass("child-menu").addClass('child-menu');
    $("#liDescription").removeClass("child-menu").addClass('child-menu');
    $("#liTemplate").removeClass("child-menu").addClass('child-menu');
    $("#liInstantRewards").removeClass("child-menu").addClass('child-menu');
    $("#liDraws").removeClass("child-menu").addClass('child-menu');
    $("#liCodes").removeClass("child-menu").addClass('child-menu');
    $("#liCost").removeClass("child-menu").addClass('child-menu');
    $("#liArtwork").removeClass("child-menu").addClass('child-menu');
    $("#liApproval").removeClass("child-menu").addClass('child-menu');
    $("#liUserJourney").removeClass("child-menu").addClass('child-menu');
    $("#liReport").removeClass("child-menu").addClass('child-menu');
    switch ($("#hidActiveGrid").val()) {
        case "liDashBoard":
            $("#liDashBoard").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liGeneral":
            $("#liGeneral").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liDescription":
            $("#liDescription").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liTemplate":
            $("#liTemplate").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liInstantRewards":
            $("#liInstantRewards").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liDraws":
            $("#liDraws").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liCodes":
            $("#liCodes").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liCost":
            $("#liCost").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liArtwork":
            $("#liArtwork").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liApproval":
            $("#liApproval").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liUserJourney":
            $("#liUserJourney").removeClass("child-menu").addClass('child-menu active');
            break;
        case "liReport":
            $("#liReport").removeClass("child-menu").addClass('child-menu active');
            break;
    }

	if ($("#hidIsEdit").val() == "0") {
		$("#frmmain :input").prop("disabled", true);
	}


});
function start() {
	$(".loader").show();
	return true;
}

