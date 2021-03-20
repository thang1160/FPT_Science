﻿$("#ckfe").change(function () {
    var val = $("#ckfe").val();
    if (val == 0) {
        $(".editfe").each(function () {
            $(this).prop("disabled", true);
        });
    } else {
        $(".editfe").each(function () {
            $(this).prop("disabled", false);
        });
    }
});
$('#ckfe').select2({
    allowClear: true
});
$('#add_author_workplace').select2({
    allowClear: true
});
$('#add_author_title').select2({
    allowClear: true
});
$('#add_author_contractType').select2({
    allowClear: true
});

$(function () {
    $(".tacgia").hide()
})
donvife = {
    1: "FPTU",
    2: "FPT Swinburne",
    3: "Fpoly",
    4: "Khác"
}

"use strict";
var uppy2;
// Class definition
var KTUppy = function () {
    const Tus = Uppy.Tus;
    //const ProgressBar = Uppy.ProgressBar;
    const StatusBar = Uppy.StatusBar;
    const FileInput = Uppy.FileInput;
    const Informer = Uppy.Informer;

    // to get uppy companions working, please refer to the official documentation here: https://uppy.io/docs/companion/
    //const Dashboard = Uppy.Dashboard;
    //const Dropbox = Uppy.Dropbox;
    //const GoogleDrive = Uppy.GoogleDrive;
    //const Instagram = Uppy.Instagram;
    //const Webcam = Uppy.Webcam;

    // Private functions

    var initUppy5 = function () {
        // Uppy variables
        // For more info refer: https://uppy.io/
        var elemId = 'kt_uppy_5';
        var id = '#' + elemId;
        var $statusBar = $(id + ' .uppy-status');
        var $uploadedList = $(id + ' .uppy-list');
        var timeout;

        uppy2 = Uppy.Core({
            debug: true,
            autoProceed: false,
            showProgressDetails: true,
            restrictions: {
                maxFileSize: 5242880, // 5mb
                maxNumberOfFiles: 2,
                minNumberOfFiles: 1,
                allowedFileTypes: ['.pdf', 'image/*', '.jpg', '.jpeg', '.png', '.gif']
            }
        });

        uppy2.use(FileInput, {
            target: id + ' .uppy-wrapper',
            pretty: false
        });
        uppy2.use(Informer, {
            target: id + ' .uppy-informer'
        });

        // demo file upload server
        uppy2.use(Tus, {
            endpoint: 'https://master.tus.io/files/'
        });
        uppy2.use(StatusBar, {
            target: id + ' .uppy-status',
            hideUploadButton: true,
            hideAfterFinish: false
        });

        $(id + ' .uppy-FileInput-input').addClass('uppy-input-control').attr('id', elemId + '_input_control');
        $(id + ' .uppy-FileInput-container').append('<label class="uppy-input-label btn btn-light-primary btn-sm btn-bold" for="' + (elemId + '_input_control') + '">Attach files</label>');

        var $fileLabel = $(id + ' .uppy-input-label');

        uppy2.on('upload', function (/*data*/) {
            $fileLabel.text("Uploading...");
            $statusBar.addClass('uppy-status-ongoing');
            $statusBar.removeClass('uppy-status-hidden');
            clearTimeout(timeout);
        });

        uppy2.on('file-added', function (file) {
            var sizeLabel = "bytes";
            var filesize = file.size;
            if (filesize > 1024) {
                filesize = filesize / 1024;
                sizeLabel = "kb";

                if (filesize > 1024) {
                    filesize = filesize / 1024;
                    sizeLabel = "MB";
                }
            }
            var uploadListHtml = '<div class="uppy-list-item" data-id="' + file.id + '"><div class="uppy-list-label">' + file.name + ' (' + Math.round(filesize, 2) + ' ' + sizeLabel + ')</div><span class="uppy-list-remove" data-id="' + file.id + '"><i class="flaticon2-cancel-music"></i></span></div>';
            $uploadedList.append(uploadListHtml);

            $statusBar.addClass('uppy-status-hidden');
            $statusBar.removeClass('uppy-status-ongoing');
            filename.push(file.name);
        });

        $(document).on('click', id + ' .uppy-list .uppy-list-remove', function () {
            var itemId = $(this).attr('data-id');
            uppy2.removeFile(itemId);
            $(id + ' .uppy-list-item[data-id="' + itemId + '"').remove();
        });
    }


    return {
        // public functions
        init: function () {
            initUppy5();
            setTimeout(function () {

            }, 2000);
        }
    };
}();

KTUtil.ready(function () {
    KTUppy.init();
});

var filename = [];

class AuthorInfoView {
    constructor(add_author_workplace, add_author_msnv, add_author_name, add_author_title, add_author_contractType, add_author_cmnd, add_author_tax
        , add_author_bank, add_author_accno, add_author_reward, add_author_note, add_author_email, id) {
        this.add_author_msnv = add_author_msnv;
        this.add_author_email = add_author_email;
        this.add_author_workplace = add_author_workplace;
        this.add_author_name = add_author_name;
        this.add_author_title = add_author_title;
        this.add_author_contractType = add_author_contractType;
        this.add_author_cmnd = add_author_cmnd;
        this.add_author_tax = add_author_tax;
        this.add_author_bank = add_author_bank;
        this.add_author_accno = add_author_accno;
        this.add_author_reward = add_author_reward;
        this.add_author_note = add_author_note;
        this.add_author_info_id = id
    }
    getHTML() {
        return `
                 <div class='col-lg-6' id='`+ this.add_author_info_id + `'>
                                <!--begin::Card-->
                                <div class='card card-custom gutter-b'>
                                    <div class='card-header'>
                                        <div class='card-title'>
                                            <h3 class='card-label'>`+ this.add_author_msnv + ` - ` + this.add_author_name + `</h3>
                                        </div>
                                        <div class='card-toolbar'>
                                            <a data-id='`+ this.add_author_info_id + `' class='edit-author btn btn-icon btn-sm btn-hover-light-danger'>
                                                <i class='far fa-edit'></i>
                                            </a>
                                            <a data-id='`+ this.add_author_info_id + `' class='del-author btn btn-icon btn-sm btn-hover-light-danger'>
                                                <i class='la la-trash'></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class='card-body author-info-card'>
                                        <div class='row'>
                                            <div class='col-lg-6 col-sm-12'>
                                                <div class='mb-7'>
                                                    <div class='d-flex justify-content-between align-items-center'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Khu Vực:</span>
                                                        <span class='text-muted font-weight-bold'>`+ this.add_author_workplace + `</span>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Chức danh:</span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_title + `</a>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Loại hợp đồng:</span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_contractType + `</a>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Mã số thuế: </span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_tax + `</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class='col-lg-6 col-sm-12'>
                                                <div class='mb-7'>
                                                    <div class='d-flex justify-content-between align-items-center'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Email:</span>
                                                        <span class='text-muted font-weight-bold'>`+ this.add_author_email + `</span>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>CMND số:</span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_cmnd + `</a>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Ngân hàng: </span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_bank + `</a>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Số tài khoản: </span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_accno + `</a>
                                                    </div>
                                                    <div class='d-flex justify-content-between align-items-cente my-1'>
                                                        <span class='text-dark-75 font-weight-bolder mr-2'>Tiền thưởng: </span>
                                                        <a href='#' class='text-muted text-hover-primary'>`+ this.add_author_reward + `</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end::Card-->
                            </div>
                            `
    }
}
//////////////////////////////////////////////////////////////////
$("#add_author_save").click(function () {
    ckfe = $("#ckfe").val()
    add_author_workplace = $("#ckfe").val()
    add_author_msnv = $("#add_author_msnv").val()
    add_author_name = $("#add_author_name").val()
    add_author_title = $("#add_author_title option:selected").text()
    add_author_contractType = $("#add_author_contractType option:selected").text()
    add_author_cmnd = $("#add_author_cmnd").val()
    add_author_tax = $("#add_author_tax").val()
    add_author_bank = $("#add_author_bank").val()
    add_author_accno = $("#add_author_accno").val()
    add_author_reward = $("#add_author_reward").val()
    add_author_note = $("#add_author_note").val()
    add_author_email = $("#add_author_mail").val()
    id = new Date().getTime()
    au = new AuthorInfoView(add_author_workplace, add_author_msnv, add_author_name,
        add_author_title, add_author_contractType, add_author_cmnd, add_author_tax,
        add_author_bank, add_author_accno, add_author_reward, add_author_note, add_author_email, id)
    $("#authors-info-container").append(au.getHTML());
    var AddAuthor = {
        name: add_author_name,
        email: add_author_email,
        bank_number: add_author_accno,
        tax_code: add_author_tax,
        bank_branch: add_author_bank,
        identification_number: add_author_cmnd,
        mssv_msnv: add_author_msnv,
        office_id: $("#ckfe option:selected").attr("name"),
        contract_id: $("#add_author_contractType").val(),
        title_id: $("#add_author_title").val(),
        people_id: $("#add_author_msnv").attr("name")
    }
    people.push(AddAuthor);

    filename = [];
    $("div").remove(".uppy-list-item");

    //var files = uppy2.getFiles();
    //var temp_array = [];
    //for (var i = 0; i < files.length; i++) {
    //    var temp = files[i].data;
    //    temp_array.push(temp);
    //    uppy2.removeFile(files[i].id);
    //}
    //files_data.push(temp_array)

    //console.log(files_data);
});
$("#authors-info-container").on('click', '.del-author', function () {
    let id = $(this).data("id")
    Swal.fire({
        title: "Xoá tác giả này?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Huỷ",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $("#" + id).remove()
        }
        //else if (result.dismiss === "cancel") {
        //}
    });
});