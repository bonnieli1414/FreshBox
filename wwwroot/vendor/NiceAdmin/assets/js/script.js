$(document).ready(function () {
    function DeleteData(id, data) {
        var message = '請問您確認是否要刪除';
        if (!!data) {
            message += ' [' + data + '] ?'
        }
        else {
            message += '此筆資料?'
        }

        Swal.fire({
            title: "<strong>您確定要刪除?</strong>",
            text: message,
            icon: 'question',
            showCancelButton: true,
            focusConfirm: false,
            closeOnConfirm: false,
            confirmButtonText: '<i class="fas fa-check pe-2"></i>確定',
            cancelButtonText: '<i class="fas fa-times pe-2"></i>取消',
        }).then(function (result) {
            if (result.isConfirmed) {
                $.ajax({
                    url: '@Url.Action("DeleteRow", ActionService.Controller, new { area = ActionService.Area })',
                    data: { "id": id },
                    type: "POST",
                    dataType: "json",
                }).done(function (value) {
                    if (value.Mode == true) {
                        location.reload(true);
                    }
                    else {
                        Swal.fire('錯誤訊息1', value.Message, 'error');
                    }
                }).fail(function (err) {
                    Swal.fire('錯誤訊息2', err, 'error');
                    console.log(err);
                });
            }
        });
    }
});