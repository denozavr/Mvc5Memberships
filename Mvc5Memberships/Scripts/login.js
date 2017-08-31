$(function () {
    //Wire up the hover event
    var loginLinkHover = $("#loginLink").hover(onLoginLinkHover);

    function onLoginLinkHover()
    {
        $("div[data-login-user-area]").addClass('open');
    }
});