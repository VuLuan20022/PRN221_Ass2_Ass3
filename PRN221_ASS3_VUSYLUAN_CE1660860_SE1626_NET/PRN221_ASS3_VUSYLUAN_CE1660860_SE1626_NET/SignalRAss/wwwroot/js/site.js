// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    LoadAppUserData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadAppUsers", function () {
        LoadAppUserData();
    })
    LoadAppUserData();
    function LoadAppUserData() {
        var tr = '';
        $.ajax({
            url: '/AppUsers/GetAppUsers',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                     <td> ${v.userID} </td>
                    <td> ${v.fullName} </td>
                    <td> ${v.address} </td>
                    <td> ${v.email} </td>
                    <td> ${v.password} </td>
                    <td>
                    <a href='../AppUsers/Edit?id=${v.userID}'>Edit </a>
                    <a href='../AppUsers/Details?id=${v.userID}'>Details </a>
                    <a href='../AppUsers/Delete?id=${v.userID}'>Delete </a>

                    </td>
                    </tr>`
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})

$(() => {
    LoadCategoryData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadCategorys", function () {
        LoadCategoryData();
    })
    LoadCategoryData();
    function LoadCategoryData() {
        var tr = '';
        $.ajax({
            url: '/PostCategories/GetCategories',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                     <td> ${v.categoryID} </td>
                    <td> ${v.categoryName} </td>
                    <td> ${v.description} </td>

                    <td>
                    <a href='../PostCategories/Edit?id=${v.categoryID}'>Edit </a>
                    <a href='../PostCategories/Details?id=${v.categoryID}'>Details </a>
                    <a href='../PostCategories/Delete?id=${v.categoryID}'>Delete </a>

                    </td>
                    </tr>`
                })
                $("#tableBodyCategory").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})   

$(() => {
    LoadPostData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadPosts", function () {
        LoadPostData();
    })
    LoadPostData();
    function LoadPostData() {
        var tr = '';
        $.ajax({
            url: '/Posts/GetPosts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                     <td> ${v.postID} </td>
                     <td> ${v.createdDate} </td>
                     <td> ${v.updatedDate} </td>
                      <td> ${v.title} </td>
                      <td> ${v.content} </td>
                      <td> ${v.publicStatus} </td>
                      <td> ${v.authorID} </td>
                      <td> ${v.categoryID} </td>
                    <td>
                    <a href='../Posts/Edit?id=${v.postID}'>Edit </a>
                    <a href='../Posts/Details?id=${v.postID}'>Details </a>
                    <a href='../Posts/Delete?id=${v.postID}'>Delete </a>

                    </td>
                    </tr>`
                })
                $("#tableBodyPost").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})  