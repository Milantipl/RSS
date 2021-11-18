function InitSelect2FromRemote(element, url, placeholder, multival, minInputLen, maximumSelectionLength, allowTaging) {
    multival = multival || !1;
    return $(element).select2({
        tags: allowTaging,
        placeholder: placeholder,
        minimumInputLength: minInputLen,
        maximumSelectionLength: maximumSelectionLength,
        multiple: multival,
        ajax: {
            url: url,
            dataType: 'json',
            data: function (params)
            { return { term: params.term } },
            processResults: function (data) { return { results: data, more: !1 } },
            cache: !0
        },
    })
}