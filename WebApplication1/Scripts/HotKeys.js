
// Shortcut binders baby
$(document).bind('keydown', 'alt+shift+e', function () {
    $('#mdlCreateEmployee').modal('show');
});
$(document).bind('keydown', 'alt+shift+h', function () {
    $('#mdlHelp').modal('show');
});
$(document).bind('keydown', 'alt+shift+d', function () {
    window.location = "CreateDesignBid.aspx";
});
$(document).bind('keydown', 'alt+shift+p', function () {
    window.location = "AddProductionPlan.aspx";
});
$(document).bind('keydown', 'alt+shift+t', function () {
    $('#mdlCreateTeam').modal('show');
});
$(document).bind('keydown', 'alt+shift+c', function () {
    $('#mdlCreateClient').modal('show');
});