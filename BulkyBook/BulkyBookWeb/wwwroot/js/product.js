var DataTable;

$(document).ready(function () {
    $('#tblData').DataTable({
        ajax: '/Admin/Product/GetAll',
        columns: [
            { data: "title" },
            { data: "isbn" },
            { data: "price" },
            { data: "author" },
            { data: "category.name" },
        ],
    });
});