$(function(){
  var Id = getQueryString("Id");


  console.log(Id);
  var urldetails = "/Handler/HandlerProject.ashx?action=GetApartmentDetails&Id="+Id;
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
              //console.log(data);
              var wlc_title = data[0].ArticleTitle;
              var wlc_content = data[0].ArticleContent;
              $("#tname").text(data[0].ArticleTitle);
              $('#news-title').html(wlc_title);
              $('#news-content').html(wlc_content);

          }
      },
      error: function() {
          alert("系统异常！");
      }
  });
})
