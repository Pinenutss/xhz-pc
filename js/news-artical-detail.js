$(function(){
  var Id = getQueryString("Id");
  console.log(Id);
  var urldetails = "/Handler/HandlerActivity.ashx?action=GetNewsDetails&Id="+Id;
  $.ajax({
      url: urldetails,
      type: "post",
      async: false,
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      success: function(data) {
          if (data.Status == "ex") {
              alert("系统异常！");
          } else {
              console.log(data);
              var news_title = data[0].NewsTitle;
              var news_content = data[0].NewsContent;
              $('#news-title').html(news_title);

              $('#news-content').html(news_content);
          }
      },
      error: function() {
          alert("系统异常！");
      }
  });
})
