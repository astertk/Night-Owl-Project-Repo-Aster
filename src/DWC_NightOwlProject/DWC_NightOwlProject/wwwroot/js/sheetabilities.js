function str(s)
{
    $("#details").append(s);
}
function confirm() 
{
    let name = $("<p></p>").text("cheese");
    $("#details").append(name);
    $("#mainstuff").append(name);
}
$("button#roll").mousedown(function()
{
    let die=$(this).text();
    die=parseInt(die);
    let result=Math.floor((Math.random()*die)+1);
    result="<p>"+result+"</p>";
    $("#result").empty();
    $("#result").append(result);
})
$("#addnewfeature").mousedown(function()
{
    let feature=$("#newfeature").val();
    let content="<p>"+feature+"</p>";
    $("#features").append(content);
})
$("#addnewweapon").mousedown(function()
{
    let weapon=$("#newweapon").val();
    let bonus=$("#newatk").val();
    let damage=$("#newdamage").val();
    let type=$("#newtype").val();
    let range=$("#newrange").val();
    let content="<tr><td>"+weapon+"</td><td>"+bonus+"</td><td>"+damage+"</td><td>"+type+"</td><td>"+range+"</td></tr>";
    $("#attacks tbody").append(content);
})
$("#addnewspell").mousedown(function()
{
    let lvl=$("#newspelllevel").val();
    let name=$("#newspell").val();
    let content="<tr><td>"+lvl+"</td><td>"+name+"</td></tr>";
    $("#spellsheet tbody").append(content);
})
$(":checkbox").mousedown(function()
{
    let newMod=2;
    let mod=$(this).parent().siblings("#modifier").text();
    if(!$(this).is(":checked"))
    {
        newMod=newMod+parseInt(mod);
    }
    else
    {
        newMod=parseInt(mod)-newMod;
    }
    if(newMod>-1)
    {
        newMod="+"+newMod;
    }
    $(this).parent().siblings("#modifier").text(newMod);
})