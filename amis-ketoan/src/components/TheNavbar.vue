<template>
  <div class="d-navbar">
        <div class="d-menu-item d-selected">
            <div class="icon_dashboard"></div>
            <div class="d-menu-text">Tổng quan</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_cash"></div>
            <div class="d-menu-text">Tiền mặt</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_bank"></div>
            <div class="d-menu-text">Tiền gửi</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_pu"></div>
            <div class="d-menu-text">Mua hàng</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_sale"></div>
            <div class="d-menu-text">Bán hàng</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_invoice"></div>
            <div class="d-menu-text">Quản lí hóa đơn</div>
        </div>
        <div class="d-menu-item d-selected">
            <div class="icon_stock"></div>
            <div class="d-menu-text">Kho</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_tools"></div>
            <div class="d-menu-text">Công cụ dụng cụ</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_fixed-assets"></div>
            <div class="d-menu-text">Tài sản cố định</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_tax"></div>
            <div class="d-menu-text">Thuế</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_price"></div>
            <div class="d-menu-text">giá thành</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_general"></div>
            <div class="d-menu-text">Tổng hợp</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_budget"></div>
            <div class="d-menu-text">Ngân sách</div>
        </div>
        <!-- <div class="d-menu-item">
            <div class="icon_report"></div>
            <div class="d-menu-text">Báo cáo</div>
        </div>
        <div class="d-menu-item">
            <div class="icon_finance"></div>
            <div class="d-menu-text">Ngân sách tài chính</div>
        </div> -->
        <div class="d-menu-item">
            <div class="icon_finance"></div>
           <router-link to="/employee">Employee</router-link>
        </div>
        <div class="d-menu-item">
            <div class="icon_finance"></div>
           <router-link to="/">Home</router-link>
        </div>
    </div>
</template>

<script>
export default {

}
</script>

<style scoped>
@import url(../style/css/layout/navbar.css);
</style>