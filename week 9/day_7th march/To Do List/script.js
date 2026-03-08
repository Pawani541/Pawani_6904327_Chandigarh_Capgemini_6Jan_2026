function addTask()
{
    var input = document.getElementById("taskInput");
    var taskText = input.value;

    if(taskText === "")
    {
        alert("Please enter a task");
        return;
    }

    var li = document.createElement("li");
    li.innerText = taskText;

    li.onclick = function()
    {
        li.classList.toggle("done");
    };

    document.getElementById("taskList").appendChild(li);

    input.value = "";
}

function clearTasks()
{
    document.getElementById("taskList").innerHTML = "";
}