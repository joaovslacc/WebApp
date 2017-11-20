$(function () {
    $("input[data-mask-type=cnpj]").mask("99.999.999/9999-99");
    $("input[data-mask-type=cep]").mask("99999-999");
    $("input[data-mask-type=cel]").mask("(99) 99999-999?9");
    $("input[data-mask-type=tel]").mask("(99) 9999-9999");
    $("input[data-mask-type=cpf]").mask("999.999.999-99");
    $("input[data-mask-type=date]").mask("99/99/9999");

   
});

$('form').submit(function (e) {
    $(this).find('input.hidden').val('');
});

$('form[data-upload!=True]').submit(function (e) {
    $(this).find('input[data-unmask=True], input[data-unmask=true]').each(function (index, element) {
        $(element).val($(element).val().replace(/\D/g, '')).unmask();
    });
});

$('form[data-upload!=True]').submit(function (e) {
    $(this).find('input[data-money-unmask=True]').each(function (index, element) {
        $(element).val($(element).val().replace('.', '')).unmask();
    });
});

