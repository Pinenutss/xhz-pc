$(function(){
  var Id = getQueryString("Id");
  var activet_type = getQueryString("activet_type");

  if( activet_type == 1 ){
    $('.side-nav').find('li').removeClass('active');
    $('#hdhd').addClass('active');
    $('.activety-hd').addClass('single-page-current');
    $('#hd_title').attr('href', 'activity.html?title=2').html('互动活动');
    $("#act1").text("互动活动");
  }else if (activet_type == 2) {
    $('.side-nav').find('li').removeClass('active');
    $('#sqhd').addClass('active');
    $('.activety-sq').addClass('single-page-current');
    $('#hd_title').attr('href', 'activity.html?title=3').html('社群活动');
    $("#act1").text("社群活动");
  }

  console.log(Id);
  console.log(activet_type);
  var urldetails = "/Handler/HandlerActivity.ashx?action=GetActivityDetails&Id="+Id;
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
              var activity_title = data[0].ActivityName;
              var activity_content = data[0].ActivityIntro;
              $('#news-title').html(activity_title);
              $('#news-content').html(activity_content);

          }
      },
      error: function() {
          alert("系统异常！");
      }
  });
})
