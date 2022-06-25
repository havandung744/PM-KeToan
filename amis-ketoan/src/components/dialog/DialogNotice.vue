<template>
  <div class="d-dialog-box">
    <div class="d-dialog">
      <div class="d-dialog-header"></div>
      <div class="d-dialog-content-box">
        <div class="d-dialog-icon-warning">
          <div class="icon_notice"></div>
        </div>
        <div class="d-dialog-body">content</div>
      </div>
      <div class="d-dialog-footer">
        <div class="d-dialog-footer-line"></div>
        <button class="d-btn" id="d-save" @click="btnCloseOnClick">Đồng ý</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "DialogNotice",
  data() {
    return {};
  },
  methods: {
    /**
     * Thực hiện đóng from thông báo
     * Author: HVDUNG (05/06/2022)
     */
    btnCloseOnClick() {
      document.getElementsByClassName("d-dialog-box")[0].style.display = "none";
    },
  },
};
</script>

<style scoped>
@import url("../../style/css/icon/icon.css");

.d-dialog-box-delete {
  display: none;
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(177, 177, 177, 0.397);
  z-index: 100;
}

.d-dialog-body {
  margin-top: 10px;
}
</style>>