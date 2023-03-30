$(function () {
    $(
        "#contactForm input,#contactForm textarea,#contactForm button"
    ).jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            // additional error messages or events
        },
        submitSuccess: function ($form, event) {
            event.preventDefault(); // prevent default submit behaviour
            // get values from FORM
            var name = $("input#name").val();
            var email = $("input#email").val();
            var subject = $("input#subject").val();
            var message = $("textarea#message").val();
            var firstName = name; // For Success/Failure Message
            // Check for white space in name for Success/Fail message
            if (firstName.indexOf(" ") >= 0) {
                firstName = name.split(" ").slice(0, -1).join(" ");
            }
            $this = $("#sendMessageButton");
            $this.prop("disabled", true); // Disable submit button until AJAX call is complete to prevent duplicate messages
            $.ajax({
                url: "/home/contact",
                type: "POST",
                data: {
                    Name: name,
                    Subject: subject,
                    Mail: email,
                    Message: message,
                },
                //processData: false,
                dataType: 'json',//Bekledi�imiz data t�r�
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',//G�nderilen data t�r�
                success: function (data) {
                    if (data == "ok") {
                        // Success message
                        $("#success").html("<div class='alert alert-success'>");
                        $("#success > .alert-success")
                            .html(
                                "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;"
                            )
                            .append("</button>");
                        $("#success > .alert-success").append(
                            $("<strong>").append(
                                 "Harika " +
                                 firstName +
                                 ", mesajini aldik. En kisa surede donus yapacagimizdan emin olabilirsin."
                             )
                        );
                        $("#success > .alert-success").append("</div>");
                        //clear all fields
                        $("#contactForm").trigger("reset");
                    }
                },
                error: function (data) {
                    if (data != "ok") {
                        // Fail message
                        $("#success").html("<div class='alert alert-danger'>");
                        $("#success > .alert-danger")
                            .html(
                                "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;"
                            )
                            .append("</button>");
                        $("#success > .alert-danger").append(
                            $("<strong>").text(
                                "�zg�n�z " +
                                firstName +
                                ", mesaj�n� �imdilik g�nderemiyoruz. L�tfen daha sonra tekrar dene!"
                            )
                        );
                        $("#success > .alert-danger").append("</div>");
                        //clear all fields
                        $("#contactForm").trigger("reset");
                    }
                },
                complete: function () {
                    setTimeout(function () {
                        $this.prop("disabled", false); // Re-enable submit button when AJAX call is complete
                    }, 1000);
                },
            });
        },
        filter: function () {
            return $(this).is(":visible");
        },
    });

    $('a[data-toggle="tab"]').click(function (e) {
        e.preventDefault();
        $(this).tab("show");
    });
});

/*When clicking on Full hide fail/success boxes */
$("#name").focus(function () {
    $("#success").html("");
});
