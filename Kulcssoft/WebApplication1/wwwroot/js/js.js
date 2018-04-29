function main() {
    document.getElementById("saveButton").addEventListener("click", send);
    document.getElementById("delButton").addEventListener("click", del);
    get();
}
function send() {
    var name = document.getElementById("Name").value;
    var email = document.getElementById("Email").value;
    if (isValid(email)) {
        var user = '{Name:'+ name+','+ 'Email:'+ email+'}';
        $.ajax({
            type: 'POST',
            data: JSON.stringify(user),
            url: 'api/Users',
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                DOMManipulator(data.itemArray);
            }
        });
    }
}
function get() {
    $.ajax({
        type: 'GET',
        url: 'api/Users',
        success: function (data) {
            data.forEach(function (element) {
                DOMManipulator(element.itemArray);
            });
        }
    });
}
function DOMManipulator(result) {
    var table = document.getElementById("table");
    var row = document.createElement("tr");
    row.id = result[0];
    result.forEach(function (info) {
        var item = document.createElement("td");
        item.innerHTML = info;
        row.appendChild(item);
    });

    row.appendChild(deleteButton(result[0]));
    table.appendChild(row);
}
function deleteButton(id) {
    var button = document.createElement("button");
    button.innerText = "X";
    button.addEventListener("click", function () { deleteUser(id);});
    return button;
}
function deleteUser(id){
    var row = document.getElementById(id);
    $.ajax({
        type: "DELETE",
        url: 'api/Users/' + id,
        success: row.remove()
    });
}
function del() {
    var id = document.getElementById("delId").value;
    if (isNaN(id)) {
        alert("Not valid id!");
    } else {
        deleteUser(id);
    }
}
function isValid(email) {
    var pattern = "[a-z0-9]+@[a-z]+[.][a-z]+";
    if (email.match(pattern)) {
        return true;
    }
    return false;
}
main();