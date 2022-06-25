<template>
  <paginate :page-count=totalPages :page-range="3" :margin-pages="1" :click-handler="clickCallback" :prev-text="'Trước'"
    :next-text="'Sau'" :container-class="'pagination'" :page-class="'page-item'" :force-page="pageNumber">
  </paginate>
</template>

<script>
import Paginate from 'vuejs-paginate-next';
export default {
  components: {
    paginate: Paginate,
  },
  props: ["totalPages"],
  methods: {
    clickCallback(value) {
      this.$emit("pageNumber", value);
    },
  }
};
</script>

<style>

.pagination {
  list-style-type: none;
  display: flex;
}

.page-item:first-child {
  margin-right: 24px;
}

.page-item:last-child {
  margin-left: 24px;
}

.page-item {
  cursor: pointer;
  width: 20px;
  height: 20px;
  margin: 8px;
  text-align: center;
}

.page-item.active {
  font-weight: 700;
  border: 1px solid #e0e0e0;
}
</style>