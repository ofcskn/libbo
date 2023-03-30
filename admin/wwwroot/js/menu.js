
var UINestable = function () {

    var control = function () {

    }();
    var updateOutput = function (e) {
        console.log(e);
        var list = e.length ? e : $(e.target),
            output = list.data('output');

        console.clear();
        //console.log(list.nestable('serialize'));


        var array = list.nestable('serialize');
        //No Child Array Ajax
        var lang = $(".btn-group .active input[type='radio']").val();

        $.each(array, function (i, v) {
            var childArray = v.children;
            if (childArray != null || chilArray != undefined) {
                console.log(childArray);
                $.each(childArray, function (index, value) {
                    console.log(index);
                    console.log(value);
                    var child2Array = value.children;
                    if (child2Array != null || child2Array != undefined) {
                        console.log(child2Array);
                        $.each(child2Array, function (index2, value2) {
                            console.log(index2);
                            console.log(value2);
                            //var child3Array = value.children;
                            $.ajax({
                                url: "/admin/menu/manage",
                                type: "POST",
                                dataType: "json",
                                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                                data: {
                                    "Lang": lang,
                                    "Permalink": value2.permalink,
                                    "Priority": i,
                                    "ParentPermalink": value.permalink,
                                }
                            }).done(function () {

                            });
                        });
                    }
                    else {
                        $.ajax({
                            url: "/admin/menu/manage",
                            type: "POST",
                            dataType: "json",
                            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                            data: {
                                "Lang": lang,
                                "Permalink": value.permalink,
                                "Priority": i,
                                "ParentPermalink": v.permalink,
                            }
                        }).done(function () {

                        });
                    }
                });
            }
            else {
                $.ajax({
                    url: "/admin/menu/manage",
                    type: "POST",
                    dataType: "json",
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: {
                        "Lang": lang,
                        "Permalink": v.permalink,
                        "Priority": i,
                    }
                }).done(function () {

                });
            }
        });
    };


    return {
        //main function to initiate the module
        init: function () {

            $('#nestable_list_menu').nestable({
                group: 1
            })
                .on('change', updateOutput);
            //updateOutput($('#nestable_list_menu').data('output', $('#nestable_list_menu_output')));
        },
    };

}();
jQuery(document).ready(function () {
    UINestable.init();
});

//Alert Hide
$('#item-control-alert').hide();

function addNestablePageItem(text, permalink) {
    return "<li class='dd-item dd3-item' data-permalink='" + permalink + "'>" +
        "<div class='dd-handle dd3-handle'> </div>" +
        "<i class='fa fa-remove dd4-handle'></i>" +
        "<div class='dd3-content'>" + text + "</div>" +
        "</li>";
}

$(document).on('click', '.list-icon-container .fa-plus', function () {
    var text = $(this).data("text");
    var permalink = $(this).data("permalink");
    var liHTML = addNestablePageItem(text, permalink);

    var formData = new FormData();
    formData.append('permalink', permalink);

    $.ajax({
        url: "/admin/menu/control",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        if (data == "yes") {

            $("#nestable_list_menu > .dd-list").append(liHTML);
            var priority = $("#nestable_list_menu > .dd-list > .dd-item").length;
            var lang = $(".btn-group .active input[type='radio']").val();
            var formData = new FormData();
            formData.append('Lang', lang);
            formData.append('Permalink', permalink);
            formData.append('Priority', priority);

            $.ajax({
                url: "/admin/menu/manage",
                type: "POST",
                processData: false,
                contentType: false,
                data: formData
            }).done(function () {
            });

        }
        else {
            setTimeout(function () { $('#item-control-alert').show().addClass('alert-danger') }, 100);
        }
    });

    //UINestable.update();

});

$(document).on('click', '.dd-list .fa-remove', function () {
    $(this).closest('li').remove();
    var permalink = $(this).data("deletepermalink");
    var formData = new FormData();
    formData.append("permalink", permalink);
    //Append(a, b) => a= name , b=value
    //dataType: 'json',
    //Ajax 
    $.ajax({

        url: "/admin/menu/delete",
        type: "POST",
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData
    }).done(function (data) {

    });
});
