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
            //pass data to action method for edit
            {
                "data": "id",
                "render":
                function (data) {
                        return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                    },
                "width": "25%"
            }


            
        ],
    });
});