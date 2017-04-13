
/*需与pagination.css,jquery.pagination.js一起使用*/
var clickpage;

function doDatePagerForControl(AllCount, pageSize, pageselectCallback, ControlId) {
    if (AllCount == 0) {
        $("#" + ControlId).html("");
    }
    else {
        $("#" + ControlId).pagination(AllCount, {

            callback: pageCallback,
            prev_text: "上一页",
            next_text: "下一页",
            //            prev_text: "&nbsp;",
            //            next_text: "&nbsp;",
            items_per_page: pageSize,
            num_display_entries: 3,
            current_page: 0, //curPage,
            num_edge_entries: 2, //两侧
            link_to: "javascript:void(0)"//url控件分页时启用参数
        });
    }
    function pageCallback(index, jq) { clickpage = index; pageselectCallback(); }
}