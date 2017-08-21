$(function () {
    var code = $(".register-code-panel input");

    function displayMessage(success, message)
    {
        var alertDiv = $(".register-code-panel .alert");
        alertDiv.text(message);
        if (success)
            alertDiv.removeClass('alert-danger').addClass('alert-success');
        else
            alertDiv.removeClass('alert-success').addClass('alert-danger');

        alertDiv.removeClass('hidden');
    }

    $(".register-code-panel button").click(function (e) {
        $(".register-code-panel .alert").addClass("hidden");

        if (code.val().length === 0)
        {
            displayMessage(false, "Enter a code");
            return;
        }

        $.post("/RegisterCode/Register", { code: code.val() },
            function (data)
            {
                displayMessage(true, "The code was successfully added. \n\r Please reload the page.");
                code.val("");
            }).fail(function(xlr, status, error){
            displayMessage(false, "Could not register the code");
        });
    });
});