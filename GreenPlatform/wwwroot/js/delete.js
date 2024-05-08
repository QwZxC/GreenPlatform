$(function () {
    $("delete_button").on("click",(function (event) {
        event.preventDefault();
        $('<div title="Confirm Box"></div>').dialog({
            open: function (event, ui) {
                $(this).html("Yes or No question?");
            },
            close: function () {
                $(this).remove();
            },
            resizable: false,
            height: 140,
            modal: true,
            buttons: {
                'Yes': function () {
                    const articleId = $(target).data('id');
                    $(this).dialog('close');
                    $.post(`articles/${articleId}`);

                },
                'No': function () {
                    $(this).dialog('close');
                }
            }
        });
    }));
});
