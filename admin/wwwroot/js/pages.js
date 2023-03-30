var mediaPath = "/uploads/";
var fileNames = "";

var FormWizard = function () {
    return {
        init: function () {
            if (!jQuery().bootstrapWizard) {
                return;
            }



            var form = $('#submit_form');
            var error = $('.alert-danger', form);
            var success = $('.alert-success', form);

            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    pageTitle: {
                        required: true
                    },
                    description: {
                        required: true
                    },
                    permaLink: {
                        required: true
                    },
                    lang: {
                        required: true
                    },
                    title: {
                        required: true
                    },
                    seoDescription: {
                        required: true
                    },
                    seoTitle: {
                        required: true
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit
                    success.hide();
                    error.show();
                    App.scrollTo(error, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    if (label.attr("for") == "gender" || label.attr("for") == "payment[]") { // for checkboxes and radio buttons, no need to show OK icon
                        label
                            .closest('.form-group').removeClass('has-error').addClass('has-success');
                        label.remove(); // remove error label here
                    } else { // display success icon for other inputs
                        label
                            .addClass('valid') // mark the current input as valid and display OK icon
                            .closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
                    }
                },

                submitHandler: function (form) {
                    success.show();
                    error.hide();
                    form[0].submit();
                    //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
                }
            });

            var displayConfirm = function () {
                $('#tab4 .form-control-static', form).each(function () {
                    var input = $('[name="' + $(this).attr("data-display") + '"]', form);
                    if (input.is(":radio")) {
                        input = $('[name="' + $(this).attr("data-display") + '"]:checked', form);
                    }
                    if (input.is(":text") || input.is("textarea")) {
                        $(this).html(input.val());
                    } else if (input.is("select")) {
                        $(this).html(input.find('option:selected').text());
                    } else if (input.is(":radio") && input.is(":checked")) {
                        $(this).html(input.attr("data-title"));
                    } else if ($(this).attr("data-display") == 'payment[]') {
                        var payment = [];
                        $('[name="payment[]"]:checked', form).each(function () {
                            payment.push($(this).attr('data-title'));
                        });
                        $(this).html(payment.join("<br>"));
                    }
                });
            }

            var handleTitle = function (tab, navigation, index) {
                var total = navigation.find('li').length;
                var current = index + 1;
                // set wizard title
                $('.step-title', $('#form_wizard_1')).text(' - ' + (index + 1) + ' / ' + total);
                // set done steps
                jQuery('li', $('#form_wizard_1')).removeClass("done");
                var li_list = navigation.find('li');
                for (var i = 0; i < index; i++) {
                    jQuery(li_list[i]).addClass("done");
                }

                if (current === 1) {
                    $('#form_wizard_1').find('.button-previous').hide();
                } else {
                    $('#form_wizard_1').find('.button-previous').show();
                }

                if (current >= total) {
                    $('#form_wizard_1').find('.button-next').hide();
                    $('#form_wizard_1').find('.button-submit').show();
                    displayConfirm();
                } else {
                    $('#form_wizard_1').find('.button-next').show();
                    $('#form_wizard_1').find('.button-submit').hide();
                }
                App.scrollTo($('.page-title'));
            }

            $('#form_wizard_1').bootstrapWizard({
                'nextSelector': '.button-next',
                'previousSelector': '.button-previous',
                'lastSelector': '.button-submit',
                onTabClick: function (tab, navigation, index, clickedIndex) {
                    return false;

                    //success.hide();
                    //error.hide();
                    //if (form.valid() == false) {
                    //    return false;
                    //}

                    //handleTitle(tab, navigation, clickedIndex);
                },
                onNext: function (tab, navigation, index) {
                    success.hide();
                    error.hide();



                    if (form.valid() == false) {
                        return false;
                    }

                    FormWizard.insertUpdate();

                    handleTitle(tab, navigation, index);
                },
                onLast: function () {
                    success.hide();
                    error.hide();

                    if (form.valid() == false) {
                        return false;
                    }

                    FormWizard.insertUpdate();

                    window.location.href = "/admin/pages/list";
                },
                onPrevious: function (tab, navigation, index) {
                    success.hide();
                    error.hide();

                    handleTitle(tab, navigation, index);
                },
                onTabShow: function (tab, navigation, index) {
                    var total = navigation.find('li').length;
                    var current = index + 1;
                    var $percent = (current / total) * 100;

                    $("#step").val(index);

                    $('#form_wizard_1').find('.progress-bar').css({
                        width: $percent + '%'
                    });
                }
            });
        },
        insertUpdate: function () {

            var sectionsJsonDataUpdate = "";
            var headerJsonData = "";
            var slidersJsonData = "";

            var formData = new FormData();

            formData.append("id", $("input[name='id']").val());
            formData.append("pageTitle", $("input[name='pageTitle']").val());
            formData.append("permaLink", $("input[name='permaLink']").val());
            formData.append("lang", $("select[name='lang']").val());
            formData.append("subMenuId", $("select[name='subMenuId']").val());
            formData.append("seoTitle", $("input[name='seoTitle']").val());
            formData.append("pageImage", $("input[name='pageImage']").val());
            formData.append("seoDescription", $("input[name='seoDescription']").val());
            formData.append("sliderOverImages", $("input[name='sliderOverImages']").val());
            formData.append("sliderOverImagesCss", $("select[name='sliderOverImagesCss']").val());
            $('#sectionsHtml').html($("#sectionsHtml").sortDivs());

            formData.append("pageRevisionId", $("input[name='pageRevisionId']").val());

            $('#sectionsHtml').children('div').each(function (i) {
                if ($(this).find("input[name='sectionName']").val() === "_slider") {
                    const links = $("ul.sortableSlides").find(".sliderLink");
                    const images = $("ul.sortableSlides").find(".btn-remove-slider-image");
                    const orders = $("ul.sortableSlides").find("input[name='sliderOrder']");

                    const mobileLinks = $("ul.sortableMobileSlides").find(".sliderLink");
                    const mobileImages = $("ul.sortableMobileSlides").find(".btn-remove-slider-image");
                    const mobileOrders = $("ul.sortableMobileSlides").find("input[name='sliderOrder']");

                    var json = "";
                    var mobileJson = "";
                    $(images).each((i, v) => {
                        json = json + `{"image":"${$(v).data("filename")}","link":"${$(links[i]).val()}","order":"${$(orders[i]).val()}"},`;
                    });
                    $(mobileImages).each((i, v) => {
                        mobileJson = mobileJson + `{"image":"${$(v).data("filename")}","link":"${$(mobileLinks[i]).val()}","order":"${$(mobileOrders[i]).val()}"},`;
                    });
                    sectionsJsonDataUpdate += `{"sectionName":"${$(this).find("input[name='sectionName']").val()}","mobileSlides":[` + mobileJson.substr(0, mobileJson.length - 1) + `],"order":"${$(this).find("input[name='order']").val()}","slides":[` + json.substr(0, json.length - 1) + "]},";
                } else {
                    sectionsJsonDataUpdate += $(this).find("input,textarea,select").serializeJSON() + ",";
                }
            });

            $('#tab3').each(function () {
                headerJsonData += $(this).find("input,textarea,select").serializeJSON() + ",";
            });
   
            $('#slidersHtml').children('div').each(function () {
                slidersJsonData += $(this).find("input,textarea,select").serializeJSON() + ",";
            });
            formData.append("headerJsonData", headerJsonData);
            formData.append("slidersJsonData", slidersJsonData);
            formData.append("sectionsJsonData", sectionsJsonDataUpdate);
            $.ajax({
                url: "/admin/pages/insertUpdate",
                type: "POST",
                processData: false,
                contentType: false,
                data: formData
            }).done(function (data) {
                $("input[name='id']").val(data.page.id);
                $("input[name='pageRevisionId']").val(data.pagesRevision.id);
                history.pushState("state", "title", "/admin/pages/manage/" + data.page.id);
            });
        },
        populateForm: function (data) {
            $("input[name='id']").val(data.id);
            $("input[name='pageTitle']").val(data.title);
            $("input[name='permaLink']").val(data.permaLink);
            $("input[name='seoTitle']").val(data.seoTitle);
            $("input[name='seoDescription']").val(data.seoDescription);
            $("input[name='sliderOverImages']").val(data.sliderOverImages);
            $("select[name='sliderOverImagesCss']").val(data.sliderOverImagesCss);

            var sectionsJsonData = jQuery.parseJSON("[" + data.sectionsJsonData + "]");
            if (sectionsJsonData.length !== 0) {
                $("#sectionsInfo").hide();
                $('#sectionsHtml').html('');
                var sortedSectionsJsonData = sortJSON(sectionsJsonData, 'order', 'asc');

                $.each(sortedSectionsJsonData, function (index, value) {
                    if (value === null) {
                        return false;
                    }
                    var sectionName = value.sectionName;
                    var url = "/admin/pages/sectionManager?sectionName=" + sectionName;
                    $.get(url, function (data) {
                        $('#sectionsHtml').append(data);

                        var lastAppendedObj = $("#sectionsHtml").children().last();

                        lastAppendedObj.attr("data-order", value.order);

                        $('.summernote').summernote(summernoteSettings);

                        lastAppendedObj.find(".portlet-title-span").html(value.title);
                        lastAppendedObj.find("input[name='title']").val(value.title);
                        lastAppendedObj.find("input[name='order']").val(value.order);
                        lastAppendedObj.find("textarea[name='summernote']").summernote("code", value.summernote);
                        lastAppendedObj.find("textarea[name='description']").val(value.description);
                        lastAppendedObj.find("input[name='buttonText']").val(value.buttonText);
                        lastAppendedObj.find("input[name='buttonLink']").val(value.buttonLink);
                        lastAppendedObj.find("input[name='images']").val(value.images);

                        lastAppendedObj.find("input[name='paramTitle1']").val(value.paramTitle1);
                        lastAppendedObj.find("input[name='paramTitle2']").val(value.paramTitle2);
                        lastAppendedObj.find("input[name='paramTitle3']").val(value.paramTitle3);
                        lastAppendedObj.find("input[name='paramTitle4']").val(value.paramTitle4);
                        lastAppendedObj.find("input[name='paramTitle5']").val(value.paramTitle5);
                        lastAppendedObj.find("input[name='paramTitle6']").val(value.paramTitle6);


                        lastAppendedObj.find("select[name='project1']").val(value.project1);
                        lastAppendedObj.find("select[name='project2']").val(value.project2);
                        lastAppendedObj.find("select[name='project3']").val(value.project3);
                        lastAppendedObj.find("select[name='project4']").val(value.project4);

                      
                        lastAppendedObj.find("select[name='team_members[]']").val(value.team_members);
                        lastAppendedObj.find("select[name='projects[]']").val(value.projects);
                        lastAppendedObj.find("select[name='partners[]']").val(value.partners);

                        lastAppendedObj.find("textarea[name='paramText1']").summernote("code", value.paramText1);
                        lastAppendedObj.find("textarea[name='paramText2']").summernote("code", value.paramText2);
                        lastAppendedObj.find("textarea[name='paramText3']").summernote("code", value.paramText3);
                        lastAppendedObj.find("textarea[name='paramText4']").summernote("code", value.paramText4);
                        lastAppendedObj.find("textarea[name='paramText5']").summernote("code", value.paramText5);
                        lastAppendedObj.find("textarea[name='paramText6']").summernote("code", value.paramText6);

                        lastAppendedObj.find("input[name='paramNumber1']").val(value.paramNumber1);
                        lastAppendedObj.find("input[name='paramNumber2']").val(value.paramNumber2);
                        lastAppendedObj.find("input[name='paramNumber3']").val(value.paramNumber3);
                        lastAppendedObj.find("input[name='paramNumber4']").val(value.paramNumber4);
                        lastAppendedObj.find("input[name='paramNumber5']").val(value.paramNumber5);
                        lastAppendedObj.find("input[name='paramNumber6']").val(value.paramNumber6);

                        lastAppendedObj.find("input[name='paramLink1']").val(value.paramLink1);
                        lastAppendedObj.find("input[name='paramLink2']").val(value.paramLink2);
                        lastAppendedObj.find("input[name='paramLink3']").val(value.paramLink3);
                        lastAppendedObj.find("input[name='paramLink4']").val(value.paramLink4);
                        lastAppendedObj.find("input[name='paramLink5']").val(value.paramLink5);
                        lastAppendedObj.find("input[name='paramLink6']").val(value.paramLink6);

                        lastAppendedObj.find("input[name='paramIcon1']").val(value.paramIcon1);
                        lastAppendedObj.find("input[name='paramIcon2']").val(value.paramIcon2);
                        lastAppendedObj.find("input[name='paramIcon3']").val(value.paramIcon3);
                        lastAppendedObj.find("input[name='paramIcon4']").val(value.paramIcon4);
                        lastAppendedObj.find("input[name='paramIcon5']").val(value.paramIcon5);
                        lastAppendedObj.find("input[name='paramIcon6']").val(value.paramIcon6);

                        lastAppendedObj.find("input[name='paramImage1']").val(value.paramImage1);
                        lastAppendedObj.find("input[name='paramImage2']").val(value.paramImage2);
                        lastAppendedObj.find("input[name='paramImage3']").val(value.paramImage3);
                        lastAppendedObj.find("input[name='paramImage4']").val(value.paramImage4);
                        lastAppendedObj.find("input[name='paramImage5']").val(value.paramImage5);
                        lastAppendedObj.find("input[name='paramImage6']").val(value.paramImage6);
                        lastAppendedObj.find("input[name='backgroundColor']").val(value.backgroundColor);
                        lastAppendedObj.find("input[name='titleColor']").val(value.titleColor);
                        lastAppendedObj.find("input[name='paramImageHover1']").val(value.paramImageHover1);
                        lastAppendedObj.find("input[name='paramImageHover2']").val(value.paramImageHover2);
                        lastAppendedObj.find("input[name='paramImageHover3']").val(value.paramImageHover3);
                        lastAppendedObj.find("input[name='paramImageHover4']").val(value.paramImageHover4);
                        lastAppendedObj.find("input[name='paramImageHover5']").val(value.paramImageHover5);
                        lastAppendedObj.find("input[name='paramImageHover6']").val(value.paramImageHover6);
                        lastAppendedObj.find("input[name='datepicker']").val(value.datepicker);
                        lastAppendedObj.find("select[name='status']").val(value.status);

                        productSelectTwo();
                        if (value.slides) {
                            $(".sortableSlides").sortable({
                                stop: (event, ui) => {
                                    $.each($(ui.item[0]).closest("ul.sortableSlides").find("li"), (i, v) => {
                                        $(v).find("input[name='sliderOrder']").val(i);
                                    });
                                }
                            });
                            $.each(value.slides, (i, s) => {
                                var imgHtml = "<a href='javascript:;' data-filename='" + s.image + "' class='btn btn-icon-only btn-remove-slider-image red'><i class='fa fa-times'></i></a>"
                                    + "<img src='" + mediaPath + s.image + "' />"
                                    + s.image;
                                const count = lastAppendedObj.find('ul.sortableSlides').find("li").length;
                                lastAppendedObj.find('.portlet-body').find('ul.sortableSlides').append($('<li class="sliderListItem" />').append(linkInputScheme(count, s.link)).append(imgHtml));
                            });
                        }
                        if (value.mobileSlides) {
                            $(".sortableMobileSlides").sortable({
                                stop: (event, ui) => {
                                    $.each($(ui.item[0]).closest("ul.sortableMobileSlides").find("li"), (i, v) => {
                                        $(v).find("input[name='sliderOrder']").val(i);
                                    });
                                }
                            });
                            $.each(value.mobileSlides, (i, s) => {
                                var imgHtml = "<a href='javascript:;' data-filename='" + s.image + "' class='btn btn-icon-only btn-remove-slider-image red'><i class='fa fa-times'></i></a>"
                                    + "<img src='" + mediaPath + s.image + "' />"
                                    + s.image;
                                const count = lastAppendedObj.find('ul.sortableMobileSlides').find("li").length;
                                lastAppendedObj.find('.portlet-body').find('ul.sortableMobileSlides').append($('<li class="sliderListItem" />').append(linkInputScheme(count, s.link)).append(imgHtml));
                            });
                        }


                        if (!isNull(value.paramImage1)) {
                            if ($.isArray(splitImages(value.paramImage1, ','))) {
                                $.each(splitImages(value.paramImage1, ','), function (i, v) {
                                    lastAppendedObj.find("div.file1").append($('<p />').html(imgFileHtml(v, 1)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file1").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage1), 1)));
                            }
                        }

                        if (!isNull(value.paramImage2)) {
                            if ($.isArray(splitImages(value.paramImage2, ','))) {
                                $.each(splitImages(value.paramImage2, ','), function (i, v) {
                                    lastAppendedObj.find("div.file2").append($('<p />').html(imgFileHtml(v, 2)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file2").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage2), 2)));
                            }
                        }

                        if (!isNull(value.paramImage3)) {
                            if ($.isArray(splitImages(value.paramImage3, ','))) {
                                $.each(splitImages(value.paramImage3, ','), function (i, v) {
                                    lastAppendedObj.find("div.file3").append($('<p />').html(imgFileHtml(v, 3)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file3").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage3), 3)));
                            }
                        }

                        if (!isNull(value.paramImage4)) {
                            if ($.isArray(splitImages(value.paramImage4, ','))) {
                                $.each(splitImages(value.paramImage4, ','), function (i, v) {
                                    lastAppendedObj.find("div.file4").append($('<p />').html(imgFileHtml(v, 4)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file4").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage4), 4)));
                            }
                        }

                        if (!isNull(value.paramImage5)) {
                            if ($.isArray(splitImages(value.paramImage5, ','))) {
                                $.each(splitImages(value.paramImage5, ','), function (i, v) {
                                    lastAppendedObj.find("div.file5").append($('<p />').html(imgFileHtml(v, 5)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file5").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage5), 5)));
                            }
                        }

                        if (!isNull(value.paramImage6)) {
                            if ($.isArray(splitImages(value.paramImage6, ','))) {
                                $.each(splitImages(value.paramImage6, ','), function (i, v) {
                                    lastAppendedObj.find("div.file6").append($('<p />').html(imgFileHtml(v, 6)));
                                });
                            }
                            else {

                                lastAppendedObj.find("div.file6").append($('<p />').html(imgFileHtml(removeLastComma(value.paramImage6), 6)));
                            }
                        }

                        if (!isNull(value.paramImageHover1)) {
                            lastAppendedObj.find("div.fileHover1").append($('<p />').html(imgFileHtmlHover(value.paramImageHover1, 1)));
                        }

                        if (!isNull(value.paramImageHover2)) {
                            lastAppendedObj.find("div.fileHover2").append($('<p />').html(imgFileHtmlHover(value.paramImageHover2, 2)));
                        }

                        if (!isNull(value.paramImageHover3)) {
                            lastAppendedObj.find("div.fileHover3").append($('<p />').html(imgFileHtmlHover(value.paramImageHover3, 3)));
                        }

                        if (!isNull(value.paramImageHover4)) {
                            lastAppendedObj.find("div.fileHover4").append($('<p />').html(imgFileHtmlHover(value.paramImageHover4, 4)));
                        }

                        if (!isNull(value.paramImageHover5)) {
                            lastAppendedObj.find("div.fileHover5").append($('<p />').html(imgFileHtmlHover(value.paramImageHover5, 5)));
                        }

                        if (!isNull(value.paramImageHover6)) {
                            lastAppendedObj.find("div.fileHover6").append($('<p />').html(imgFileHtmlHover(value.paramImageHover6, 6)));
                        }

                        lastAppendedObj.find("input[name='subTitle']").val(value.subTitle);
                        lastAppendedObj.find("input[name='youtubeLink']").val(value.youtubeLink);
                        lastAppendedObj.find("input[name='css']").val(value.css);
                        lastAppendedObj.find("input[name='headerCss']").val(value.headerCss);


                        collapePanel(lastAppendedObj);

                        if (value.images != 0) {
                            $.each(splitImages(value.images, ','), function (i, v) {
                                var imgHtml = "<a href='javascript:;' data-filename='" + v + "' class='btn btn-icon-only btn-remove red'><i class='fa fa-times'></i></a>"
                                    + "<img src='" + mediaPath + v + "' />"
                                    + v;
                                lastAppendedObj.find('div.files').append($('<p />').html(imgHtml));
                            });
                        }
                    });
                });
            }

            var headerJsonData = jQuery.parseJSON("[" + data.headerJsonData + "]")[0];

            $("input[name='headerTitle']").val(headerJsonData.headerTitle);
            $("input[name='headerSubTitle']").val(headerJsonData.headerSubTitle);
            $("input[name='headerVideo']").val(headerJsonData.headerVideo);
            $("input[name='headerImage']").val(headerJsonData.headerImage);
            $("input[name='headerBackgroundColor']").val(headerJsonData.headerBackgroundColor);
            $("input[name='headerBackgroundColorp']").val(headerJsonData.headerBackgroundColor);
            //headerBackgroundColor
            if (headerJsonData.headerVideo != '') {
                var headerFileVideo = "<a href='javascript:;' data-filename='" + headerJsonData.headerVideo + "' class='btn btn-icon-only btn-remove-header-video red'><i class='fa fa-times'></i></a>&nbsp;"
                    + headerJsonData.headerVideo;
                $('#videoFile').append($('<p />').html(headerFileVideo));
            }

            if (headerJsonData.headerImage != '') {
                var headerFileImage = "<a href='javascript:;' data-filename='" + headerJsonData.headerImage + "' class='btn btn-icon-only btn-remove-header-image red'><i class='fa fa-times'></i></a>"
                    + "<img src='" + mediaPath + headerJsonData.headerImage + "' />"
                    + headerJsonData.headerImage;
                $('#imageFile').append($('<p />').html(headerFileImage));
            }


            $("input[name='sliderOverImages']").val(data.sliderOverImages);

            if (data.sliderOverImages != '') {
                var sliderOverImages = "<a href='javascript:;' data-filename='" + data.sliderOverImages + "' class='btn btn-icon-only btn-remove-sliderOverImages red'><i class='fa fa-times'></i></a>"
                    + "<img src='" + mediaPath + data.sliderOverImages + "' />"
                    + headerJsonData.headerImage;
                $('#sliderOverImagesFile').append($('<p />').html(sliderOverImages));
            }


            var sliderJsonData = jQuery.parseJSON("[" + data.slidersJsonData + "]");

            if (sliderJsonData.length != 0) {
                $("#slidersInfo").hide();
                $('#slidersHtml').html('');
                var sortedSlidersJsonData = sortJSON(sliderJsonData, 'order', 'asc');

                $.each(sortedSlidersJsonData, function (index, value) {

                    var url = "/admin/pages/sectionManager?sectionName=_slider";

                    $.get(url, function (data) {
                        $('#slidersHtml').append(data);

                        var lastAppendedObj = $("#slidersHtml").children().last();
                        lastAppendedObj.attr("data-order", value.order);
                        lastAppendedObj.find("textarea[name='title']").val(value.title);
                        lastAppendedObj.find("textarea[name='description']").val(value.description);
                        lastAppendedObj.find("input[name='images']").val(value.images);
                        lastAppendedObj.find("input[name='video']").val(value.video);


                        if (value.typedTitle == "1") {
                            lastAppendedObj.find("input[type='checkbox']").prop('checked', true);
                        }

                        collapePanel(lastAppendedObj);

                        if (value.images != 0) {
                            $.each(splitImages(value.images, ','), function (i, v) {
                                var imgHtml = "<a href='javascript:;' data-filename='" + v + "' class='btn btn-icon-only btn-remove-slider-image red'><i class='fa fa-times'></i></a>"
                                    + "<img src='" + mediaPath + v + "' />"
                                    + v;
                                lastAppendedObj.find('div.imageSliderFile').append($('<p />').html(imgHtml));
                            });
                        }

                        if (value.video != 0) {
                            $.each(splitImages(value.video, ','), function (i, v) {
                                var imgHtml = "<a href='javascript:;' data-filename='" + v + "' class='btn btn-icon-only btn-remove-slider-video red'><i class='fa fa-times'></i></a>"
                                    + v;
                                lastAppendedObj.find('div.videoSliderFile').append($('<p />').html(imgHtml));
                            });
                        }
                    });
                });
            }
        }
    };
}();

$(document).on('click', '#fileUploadHeaderVideo', function () {
 
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $('input[name="headerVideo"]').val(data.result.newFileName);
            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-header-video red'><i class='fa fa-times'></i></a>&nbsp;"
                + data.result.oldFileName;
            $("#videoFile").html("");
            $('#videoFile').append($('<p />').html(imgHtml));
            //removeFile(data.result.oldFileName);
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '#fileUploadHeaderImage', function () {
  
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $('input[name="headerImage"]').val(data.result.newFileName);
            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-header-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            $("#imageFile").html("");
            $('#imageFile').append($('<p />').html(imgHtml));

        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});
var imgFileHtml = function (newFileName, id) {
    return "<a href='javascript:;' data-filename='" + newFileName + "' data-id='" + id + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
        + "<img src='" + mediaPath + newFileName + "' />"
        + newFileName;
}

var imgFileHtmlHover = function (newFileName, id) {
    return "<a href='javascript:;' data-filename='" + newFileName + "' data-id='" + id + "' class='btn btn-icon-only btn-remove-image-hover red'><i class='fa fa-times'></i></a>"
        + "<img src='" + mediaPath + newFileName + "' />"
        + newFileName;
}

var removeLastChar = function (value, char) {

    if (!value || !value.length) { return; }

    var lastChar = value.slice(-1);
    if (lastChar == char) {
        value = value.slice(0, -1);
    }
    return value;
}

var splitImages = function (value, char) {

    if (!value || !value.length) { return; }

    if (value.indexOf(char) > 0)
        return removeLastChar(value, char).split(',');
    else
        return removeLastChar(value, char);
}

var removeLastComma = function (value) {
    return value.replace(/,\s*$/, "");
}


jQuery(document).ready(function () {

    FormWizard.init();
    $('.js-reload-details-sections').on('click', function (evt) {
        evt.preventDefault();
        evt.stopPropagation();
        $.get($(this).data('url'), function (data) {
            $('#sectionsHtml').append(data);
            $("#sectionsHtml .portlet-body").hide();
            $("#sectionsHtml .tools .collapse").removeClass("collapse").addClass("expand");

            var lastAppendedObj = $("#sectionsHtml").children().last();

            orderElements();

            expandPanel(lastAppendedObj);

            productSelectTwo();
            $("#sectionsInfo").hide();
            $('.summernote').summernote(summernoteSettings);
        });


        fileNames = "";

        FormWizard.insertUpdate();
    });

    $('.js-reload-details-sliders').on('click', function (evt) {
        evt.preventDefault();
        evt.stopPropagation();

        $.get($(this).data('url'), function (data) {
            $('#slidersHtml').append(data);

            $("#slidersHtml .portlet-body").hide();
            $("#slidersHtml .tools .collapse").removeClass("collapse").addClass("expand");

            var lastAppendedObj = $("#slidersHtml").children().last();

            orderSlidersElements();

            expandPanel(lastAppendedObj);

            $("#slidersInfo").hide();

            FormWizard.insertUpdate();
        });


    });

    $('#large').on('show.bs.modal', function (e) {
        var button = $(e.relatedTarget)
        $("#imgSectionPreview").attr("src", button.data("preview"))
    })

});

function changeTitle(obj) {
    $(obj).closest('.portlet').find(".portlet-title-span").html($(obj).val());
}

function collapePanel(obj) {
    obj.find(".tools .collapse").removeClass("collapse").addClass("expand");
    obj.children(".portlet-body").hide();
}

function expandPanel(obj) {
    obj.find(".tools .expand").removeClass("expand").addClass("collapse");
    obj.children(".portlet-body").show();
}

function orderElements() {
    $('#sectionsHtml').children('div').each(function () {
        $(this).find("input[name='order']").val($(this).index());
        $(this).attr("data-order", $(this).index());
    });
}


$(document).on('click', '.sectionFileUpload', function () {
    $(this).fileupload({
        url: "/media/insert",
        dataType: 'json',
        done: function (e, data) {

            fileNames = $(this).closest('.portlet').find('input[name="images"]').val();

            fileNames += data.result.newFileName + ",";

            $(this).closest('.portlet').find('input[name="images"]').val(fileNames);

            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            $(this).closest('.portlet-body').find('div.files').append($('<p />').html(imgHtml));


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});


$(document).on('click', '.sectionSingleFileUpload', function () {
    $(this).fileupload({
        url: "/media/insert",
        dataType: 'json',
        done: function (e, data) {
            $(this).closest('.portlet').find('input[name="images"]').val(data.result.newFileName + ",");
            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            $(this).closest('.portlet-body').find('div.files').html("");
            $(this).closest('.portlet-body').find('div.files').append($('<p />').html(imgHtml));
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '.sectionMultipleFileUpload', function () {
    const id = $(this).data("id");
    const hidden = $(this).data("hidden");

    $(this).fileupload({
        url: "/media/insert",
        dataType: 'json',
        done: function (e, data) {
            fileNames = $(this).closest('.portlet').find('input[name="' + hidden + '"]').val();
            fileNames += data.result.newFileName + ",";

            $(this).closest('.portlet').find('input[name="' + hidden + '"]').val(fileNames);
            var imgHtml = "<a href='javascript:;' data-id='" + id + "' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            $(this).closest('.portlet-body').find('div.file' + id).append($('<p />').html(imgHtml));
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '.sectionInsideFileUpload', function () {
    var id = $(this).data("id");

    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            $(this).closest('.portlet').find('input[name="paramImage' + id + '"]').val(data.result.newFileName);

            var imgHtml = "<a href='javascript:;' data-id='" + id + "' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;

            $(this).closest('.portlet-body').find('div.file' + id).html("");
            $(this).closest('.portlet-body').find('div.file' + id).append($('<p />').html(imgHtml));


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});



$(document).on('click', '#fileUploadPageImage', function () {
    $(this).fileupload({
        url: "/media/insert",
        dataType: 'json',
        done: function (e, data) {
            $('input[name="pageImage"]').val(data.result.newFileName);
            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-pageImage red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            $('#pageImageFile').append($('<p />').html(imgHtml));

        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});


$(document).on('click', '.btn-remove', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();
    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

        var currentValue = obj.closest('.portlet').find('input[name="images"]').val();
        var newValue = currentValue.replace(fileName + ",", '');
        obj.closest('.portlet').find('input[name="images"]').val(newValue);

        obj.closest('p').remove();
    });
});

$(document).on('click', '.btn-remove-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');
    var id = obj.data('id');
    var formData = new FormData();
    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

        var currentValue = obj.closest('.portlet').find('input[name="paramImage' + id + '"]').val();
        var newValue = currentValue.replace(fileName + ",", '');


        obj.closest('.portlet').find('input[name="paramImage' + id + '"]').val("");

        obj.closest('p').remove();
    });
});


$(document).on('click', '.btn-remove-slider-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        obj.closest("li").remove();
    });
});

$('#sectionsHtml').on("DOMNodeInserted", () => {
    if ($(".sortableSlides").length > 0) {
        $(".sortableSlides").sortable({
            stop: (event, ui) => {
                $.each($(ui.item[0]).closest("ul.sortableSlides").find("li"), (i, v) => {
                    $(v).find("input[name='sliderOrder']").val(i);
                });
            }
        });
    }
    if ($(".sortableMobileSlides").length > 0) {
        $(".sortableMobileSlides").sortable({
            stop: (event, ui) => {
                $.each($(ui.item[0]).closest("ul.sortableMobileSlides").find("li"), (i, v) => {
                    $(v).find("input[name='sliderOrder']").val(i);
                });
            }
        });
    }
});

$(document).on('click', '.remove', function (evt) {
    orderElements();
});

function sortJSON(data, key, way) {
    return data.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        if (way === 'asc') { return ((x < y) ? -1 : ((x > y) ? 1 : 0)); }
        if (way === 'desc') { return ((x > y) ? -1 : ((x < y) ? 1 : 0)); }
    });
}

jQuery.fn.sortDivs = function sortDivs() {
    $("> div", this[0]).sort(dec_sort).appendTo(this[0]);
    function dec_sort(a, b) { return ($(b).data("order")) < ($(a).data("order")) ? 1 : -1; }
}


function convertToSlug(Text) {
    return Text
        .toLowerCase()
        .replace(/[^\w ]+/g, '')
        .replace(/ +/g, '-');
}

var slug = function (str) {
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc
    var from = "ÁÄÂÀÃÅČÇĆĎÉĚËÈÊẼĔȆÍÌÎÏŇÑÓÖÒÔÕØŘŔŠŤÚŮÜÙÛÝŸŽáäâàãåčçćďéěëèêẽĕȇíìîïňñóöòôõøðřŕšťúůüùûýÿžþÞĐđßÆa·/_,:;";
    var to = "AAAAAACCCDEEEEEEEEIIIINNOOOOOORRSTUUUUUYYZaaaaaacccdeeeeeeeeiiiinnooooooorrstuuuuuyyzbBDdBAa------";
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-'); // collapse dashes

    return str;
};


$(document).on('click', '.sliderFileUpload', function () {
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {
            fileNames = $(this).closest('.portlet').find('input[name="images"]').val();

            fileNames = data.result.newFileName + ",";

            $(this).closest('.portlet').find('input[name="images"]').val(fileNames);

            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-slider-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;

            $(this).closest('.portlet-body').find('div.imageSliderFile').html("");
            $(this).closest('.portlet-body').find('div.imageSliderFile').append($('<p />').html(imgHtml));

        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

$(document).on('click', '.sliderVideoUpload', function () {
    $(this).fileupload({
        url: "/admin/media/insert",
        dataType: 'json',
        done: function (e, data) {

            fileNames = $(this).closest('.portlet').find('input[name="video"]').val();

            fileNames = data.result.newFileName + ",";

            $(this).closest('.portlet').find('input[name="video"]').val(fileNames);

            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-slider-video red'><i class='fa fa-times'></i></a>"
                + data.result.oldFileName;
            $(this).closest('.portlet-body').find('div.videoSliderFile').html("");
            $(this).closest('.portlet-body').find('div.videoSliderFile').append($('<p />').html(imgHtml));


        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});
$(document).on('click', '.sliderMobileFileUpload', function () {
    $(this).fileupload({
        url: "/media/insert",
        dataType: 'json',
        done: function (e, data) {
            var imgHtml = "<a href='javascript:;' data-filename='" + data.result.newFileName + "' class='btn btn-icon-only btn-remove-slider-image red'><i class='fa fa-times'></i></a>"
                + "<img src='" + mediaPath + data.result.newFileName + "' />"
                + data.result.oldFileName;
            const count = $(this).closest('.portlet-body').find('ul.sortableMobileSlides').find("li").length;
            $(this).closest('.portlet-body').find('ul.sortableMobileSlides').append($('<li class="sliderListItem" />').append(linkInputScheme(count, "")).append(imgHtml));
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
});

function linkInputScheme(count, value) {
    return `<input type="text" style="margin-bottom:20px;width:50%;" 
            class="form-control sliderLink"  placeholder="Link" name="link" value="${value}"/>
            <input type="hidden" name="sliderOrder" value="${count}"/>`;
}
function productSelectTwo() {
    $("select.productSelectTwo").each((i, v) => {
        const ids = $(v).closest("div").find("input[type='hidden']").val();
        if (ids !== "") {
            $.post("/admin/pages/getProductsById", { ids: ids }).then((res) => {
                $(v).empty();
                res.forEach((product) => {
                    $(v).append(`<option selected value="${product.id}" title="${product.title}">${product.title}</option>`);
                });
            }).then(() => {
                $(v).select2({
                    ajax: {
                        url: "/admin/pages/getProducts",
                        dataType: 'json',
                        data: function (params) {
                            return {
                                q: params.term
                            };
                        },
                        processResults: function (data, params) {
                            return {
                                results: data
                            };
                        },
                        cache: true
                    },
                    placeholder: 'Ürün aramak için yazın',
                    minimumInputLength: 3,
                    templateResult: formatRepo,
                    templateSelection: formatRepoSelection
                }).val($(v).closest("div").find("input[type='hidden']").val().trim(',').split(',')).trigger('change');
                $(".select2-container")
                    .removeAttr("class")
                    .attr("class", "select2 select2-container select2-container--default select2-container--below");
                $(v).change(e => {
                    var str = '';
                    $(e.currentTarget).val().forEach(v => {
                        str = str + v + ',';
                    });
                    $(e.currentTarget).closest("div").find("input[type='hidden']").val(str);
                });
            });
        } else {
            $(v).select2({
                ajax: {
                    url: "/admin/pages/getProducts",
                    dataType: 'json',
                    data: function (params) {
                        return {
                            q: params.term
                        };
                    },
                    processResults: function (data, params) {

                        return {
                            results: data
                        };
                    },
                    cache: true
                },
                placeholder: 'Ürün aramak için yazın',
                minimumInputLength: 3,
                templateResult: formatRepo,
                templateSelection: formatRepoSelection
            });
            $(".select2-container")
                .removeAttr("class")
                .attr("class", "select2 select2-container select2-container--default select2-container--below");
            $(v).change(e => {
                var str = '';
                $(e.currentTarget).val().forEach(v => {
                    str = str + v + ',';
                });
                $(e.currentTarget).closest("div").find("input[type='hidden']").val(str);
            });
        }

    });
}

$(document).on('click', '.btn-remove', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

        var currentValue = obj.closest('.portlet').find('input[name="images"]').val();
        var newValue = currentValue.replace(fileName + ",", '');
        obj.closest('.portlet').find('input[name="images"]').val(newValue);

        obj.closest('p').remove();
    });
});


$(document).on('click', '.btn-remove-slider-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

        var currentValue = obj.closest('.portlet').find('input[name="images"]').val();
        var newValue = currentValue.replace(fileName + ",", '');
        obj.closest('.portlet').find('input[name="images"]').val(newValue);

        obj.closest('p').remove();
    });
});

$(document).on('click', '.btn-remove-slider-video', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {

        var currentValue = obj.closest('.portlet').find('input[name="video"]').val();
        var newValue = currentValue.replace(fileName + ",", '');
        obj.closest('.portlet').find('input[name="video"]').val(newValue);

        obj.closest('p').remove();
    });
});

$(document).on('click', '.btn-remove-header-video', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('input[name="headerVideo"]').val("");
        obj.closest('p').remove();
    });
});

$(document).on('click', '.btn-remove-header-image', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('input[name="headerImage"]').val("");
        obj.closest('p').remove();
    });
});

$(document).on('click', '.btn-remove-sliderOverImages', function (evt) {
    evt.preventDefault();
    evt.stopPropagation();

    var obj = $(this);

    var fileName = obj.data('filename');

    var formData = new FormData();

    formData.append("fileName", fileName);

    $.ajax({
        url: "/admin/media/delete",
        type: "POST",
        processData: false,
        contentType: false,
        data: formData
    }).done(function (data) {
        $('input[name="sliderOverImages"]').val("");
        obj.closest('p').remove();
    });
});
function formatRepo(repo) {
    if (repo.loading) {
        return repo.text;
    }
    var $container = $(
        "<div class='select2-result-repository clearfix'>" +
        "<div class='select2-result-repository__avatar'><img src='" + mediaPath + repo.images.trim(',').split(',')[0] + "' /></div>" +
        "<div class='select2-result-repository__title'></div>" +
        "<div class='select2-result-repository__forks'><i class='fa fa-money' aria-hidden='true'></i> </div>" +
        "<div class='select2-result-repository__stargazers'><i class='fa fa-truck' aria-hidden='true'></i> </div>" +
        "<div class='select2-result-repository__category'><i class='fa fa-star' aria-hidden='true'></i> </div>"+
        "</div>" +
        "</div>" +
        "</div>"
    );

    $container.find(".select2-result-repository__title").text(repo.title);
    $container.find(".select2-result-repository__forks").append(repo.price + " TL");
    $container.find(".select2-result-repository__stargazers").append(repo.firmTitle);
    $container.find(".select2-result-repository__category").append(repo.categoryName);

    return $container;
}

function formatRepoSelection(repo){
    return repo.title;
}
