// Search box Product filtering
$(document).ready(function () {
    // activate filter and read input value when key up
    $("#product-search-box").keyup(function () {
        var searchString = $("#product-search-box").val().toLowerCase();
        // go through each product title in card to compare whether it starts with input value and
        // hide non-matching cards
        $(".card-text-title").each(function () {
            var productTitle = $(this).text().trim().toLowerCase();
            if (!productTitle.startsWith(searchString)) {
                console.log("Search string: " + searchString + " product: " + productTitle);
                $(this).closest(".product-card").hide();
            }
            else {
                $(this).closest(".product-card").show();
            }
        });
    });
});