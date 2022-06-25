<template>
  <div class="d-content">
    <div class="d-page-header">
      <div class="d-page-title">Nhân viên</div>
      <button class="d-btn d-btn" id="d-btn-add-employee" @click="btnAddOnclick">
        Thêm mới nhân viên
      </button>
    </div>
    <div class="d-page-toolbar">
      <div class="d-toolbar-left">
        <input type="text" class="d-input" v-on:keyup="autoSearch" v-model="textSearch"
          placeholder="Tìm kiếm theo mã, tên nhân viên" />
        <div class="icon_search" @click="btnSearchOnClick"></div>
      </div>
      <div class="icon_refresh" @click="btnRefreshOnClick"></div>
      <div class="icon_excel" @click="btnExportExcel"></div>
    </div>

    <div class="d-grid">
      <div class="d-grid-table">
        <table class="d-table">
          <thead>
            <tr>
              <!-- <th class="text-align-left ok">STT</th> -->
              <!-- <th class="text-align-center"><input type="checkbox"></th> -->
              <th class="text-align-center">
                <input type="checkbox" />
              </th>
              <th class="text-align-left">
                MÃ NHÂN VIÊN
              </th>
              <th class="text-align-left">
                TÊN NHÂN VIÊN
              </th>
              <th class="text-align-left">
                GIỚI TÍNH
              </th>
              <th class="text-align-center">NGÀY SINH</th>
              <th class="text-align-left">
                SỐ CMND
              </th>
              <th class="text-align-left">
                CHỨC DANH
              </th>
              <th class="text-align-left">
                TÊN ĐƠN VỊ
              </th>
              <th class="text-align-left">
                SỐ TÀI KHOẢN
              </th>
              <th class="text-align-left">
                TÊN NGÂN HÀNG
              </th>
              <th class="text-align-left">
                CHI NHÁNH TK NGÂN HÀNG
              </th>
              <th class="text-align-center">
                CHỨC NĂNG
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="employee in employees" :key="employee.EmployeeId" @dblclick="trOnDoubleClick(employee)">
              <td id="d-td" class="text-align-center">
                <input type="checkbox" />
              </td>
              <td class="text-align-left">{{ employee.EmployeeCode }}</td>
              <td class="text-align-left">{{ employee.EmployeeName }}</td>
              <td class="text-align-left">{{ employee.GenderName }}</td>
              <td class="text-align-left">
                {{ formatDate(employee.DateOfBirth) }}
              </td>
              <td class="text-align-left">{{ employee.IdentityNumber }}</td>
              <td class="text-align-left">{{ employee.EmployeePosition }}</td>
              <td class="text-align-left">{{ employee.DepartmentName }}</td>
              <td class="text-align-left">{{ employee.BankAccountNumber }}</td>
              <td class="text-align-left">{{ employee.BankName }}</td>
              <td class="text-align-left">{{ employee.BankBranchName }}</td>
              <td class="text-align-center">
                <div class="d-function">
                  <div class="d-text">Sửa</div>
                  <div class="d-dropdown_box" @click="dropdownClick(employee, $event)">
                    <div class="icon_dropdown"></div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="d-paging">
        <div class="d-paging-left">
          Tổng : <b>{{ this.count }}</b> bản ghi
        </div>
        <div class="d-paging-center"></div>
        <div class="d-paging-right">
          <div class="d-row">
            <select class="d-combobox" v-model="pageSize">
              <option value="10">10 bản ghi trên 1 trang</option>
              <option value="20">20 bản ghi trên 1 trang</option>
              <option value="30">30 bản ghi trên 1 trang</option>
              <option value="50">50 bản ghi trên 1 trang</option>
              <option value="100">100 bản ghi trên 1 trang</option>
            </select>
          </div>
          <div class="d-row">
            <PaginateList :totalPages=totalPages @pageNumber="pageNumber"></PaginateList>
          </div>
        </div>
      </div>
    </div>
    <!-- showDialog là props của file detail, giá trị nhận là isShowDialog của file EmployeeList -->
    <!-- @isShowDialog là cách ta định nghĩa một phương thức để từ bên detail có thể gọi sang bên list thông qua $emit -->
    <!-- "employee" là cách ta truyền trực tiếp từ cliend chứ không thông qua server-->
    <EmployeeDetail @changeValueEmployees="changeValueEmployees" @changeValueFormMode="changeValueFormMode"
      v-show="isShowDialog" @isShowDialog="toggleEmployeeDialog" :employeeIdSelected="employeeIdSelected"
      :formMode="formDetailMode" :pageNumberSelected="pageNumberSelected" :pageSize="pageSize"
      @changeCount="changeCount" :textSearch="textSearch">
    </EmployeeDetail>
    <!-- thực hiện hiển thị thông báo khi validate không hợp lệ-->
    <DialogNotice></DialogNotice>
    <!-- thực hiện hiển thị carnb báo khi thực hiện xóa nhân viên-->
    <DialogDelete @changeValueEmployees="changeValueEmployees" :employeeIdForDelete="employeeIdForDelete"
      @pageNumber="pageNumber" :pageNumberSelected="pageNumberSelected" :pageSize="pageSize" :textSearch="textSearch"
      @changeCount="changeCount"></DialogDelete>

    <!-- thực hiện hiển thị dropdown khi click -->
    <DropDown v-show="isShowDropDown" @isShowDropDown="toggleDropDown" id="d-dropDown"
      :employeeCodeForDelete="employeeCodeForDelete" @isShowDialog="toggleEmployeeDialog"></DropDown>
    <!-- thành phần thực hiện loading -->
    <div class="loading" v-show="isLoading">
      <div class="loading-icon"></div>
    </div>
  </div>
</template>

<script>
import DropDown from "../page/DropDownList.vue";
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail.vue";
import DialogNotice from "../dialog/DialogNotice.vue";
import PaginateList from '../common/PaginateList.vue';
import DialogDelete from '../dialog/DialogDelete.vue'
export default {
  name: "EmployeeList",
  components: {
    DropDown,
    EmployeeDetail,
    DialogNotice,
    PaginateList,
    DialogDelete
  },

  /**
   * Thực hiện khởi tạo đối tượng employees
   * Author: HVDUNG (05/06/2022)
   */
  created() {
    // giá trị khởi tạo ban đầu dành cho việc hiển thị
    this.pagination(10, 1, "");
  },

  watch: {
    // cập nhật lại tổng số bản ghi khi có sự thay đổi
    count: function (value) {
      this.count = value;
    },

    // thực hiện focus nhiều lần vào cùng 1 tr thì không bị mất focus
    isShowDialog: function () {
      if (this.isShowDialog == false)
        this.employeeIdSelected = {};
    },

    // kiểm tra sự thay đổi của pageSize và cập nhật lại dữ liệu
    pageSize: function (value) {
      this.pagination(value, 1, this.textSearch);
    },

    // kiểm tra sự thay đổi của pageNumberSelected và cập nhật lại dữ liệu
    pageNumberSelected: function (value) {
      this.pagination(this.pageSize, value, this.textSearch);
    }
  },

  methods: {
    /**
     * thực hiện hiển thị form dropdown khi click và lấy được mã nhân viên được click
     * Author: HVDUNG (10/06/2022)
     */
    dropdownClick(employee, event) {
      this.toggleDropDown();
      var x = event.pageX - 105;
      var y = event.pageY + 12;
      document.getElementById('d-dropDown').style.left = `${x}px`;
      document.getElementById('d-dropDown').style.top = `${y}px`;
      this.employeeIdForDelete = employee.EmployeeId;
      this.employeeCodeForDelete = employee.EmployeeCode;
      this.employeeIdSelected = null;
    },

    /**
     * Thực hiện kiểm tra page đang chọn
     * @param {int} value giá trị
     * Author: HVDUNG (10/06/2022)
     */
    pageNumber(value) {
      this.pageNumberSelected = value;
    },

    /**
     * Thực hiện phân trang
     * @param {int} pageSize số bản ghi/trang
     * @param {int} pageNumber trang thứ bao nhiêu
     * @param {string} textSearch nội dung tìm kiếm
     * Author: HVDUNG (08/06/2022) 
     */
    async pagination(pageSize, pageNumber, textSearch) {
      var me = this;
      try {
        me.isLoading = true;
        await axios
          .get(`http://localhost:22454/api/v1/Employees/filter?pageSize=${pageSize}&pageNumber=${pageNumber}&employeeFilter=${textSearch}`)
          .then((response) => {
            console.log(response.data);
            me.employees = response.data.Data;
            me.totalPages = response.data.TotalPages;
            me.count = response.data.TotalRecords;
          })
          .catch(function (error) {
            console.log(error);
          });
        me.isLoading = false;
      } catch (error) {
        me.isLoading = false;
        console.log(error);
      }
    },

    /**
     * Thực hiện tự động tìm kiếm khi search
     * Author: VHDUNG (08/06/2022)
     */
    autoSearch() {
      var me = this;
      if (me.textSearch != "") {
        if (me.globalTimeout != null) {
          clearTimeout(me.globalTimeout);
        }
        me.globalTimeout = setTimeout(function () {
          me.globalTimeout = null;
          me.pagination(me.pageSize, 1, me.textSearch);
        }, 500);
      } else
        me.pagination(me.pageSize, me.pageNumberSelected, "");
    },

    /**
     * thực hiện build lại employees cho cha sau khi nhận sự thay đổi ở các componants khác (vd: EmployeeDeatails, DropDownList)
     * Author: HVDUNG(06/06/2022)
     */
    changeValueEmployees(value) {
      this.employees = value;
    },

    /**
    * thực hiện gán lại value cho formMode
    * Author: HVDUNG(06/06/2022)
    */
    changeValueFormMode(value) {
      this.formDetailMode = value;
    },

    /**
     * thực hiện build lại tổng số bản ghi khi xóa đối tượng
     * Author: HVDUNG(06/06/2022)
     */
    changeCount(value) {
      this.count = value;
    },

    /**
     * Thực hiện show form detail khi click
     * Author: HVDUNG(05/06/2022)
     */
    async btnAddOnclick() {
      // gán lại giá trị cho fromDetailMode là thêm mới
      this.formDetailMode = 1;
      // thực hiện xóa đi đường viền đỏ
      document.getElementById("EmployeeCode").classList.remove("d-input-error");
      document.getElementById("EmployeeName").classList.remove("d-input-error");
      // thực hiện ẩn form dropdown nếu đang mở
      document.getElementsByClassName("dropdown")[0].style.display = "none";
      // thực hiện hiển thị form chi tiết
      this.toggleEmployeeDialog();
      // gán lại giá trị cho employeeIdSelected
      this.employeeIdSelected = null;
    },

    /**
     * Thực hiện hiển thị thông tin nhân viên
     * @param {*} employee là đối tượng emplye Được lấy ở v-for
     * building ra data của employee
     * Author: HVDUNG(05/06/2022)
     */
    trOnDoubleClick(employee) {
      // gán chế độ là update
      this.formDetailMode = 0;
      // thực hiện xóa đi đường viền đỏ
      document.getElementById("EmployeeCode").classList.remove("d-input-error");
      document.getElementById("EmployeeName").classList.remove("d-input-error");
      // thực hiện ẩn form dropdown nếu đang mở
      document.getElementsByClassName("dropdown")[0].style.display = "none";
      // hiển thị form chi tiết
      this.toggleEmployeeDialog();
      this.employeeIdSelected = employee.EmployeeId;
    },

    // thực hiện việc ẩn hiện form chi tiết
    // Author: HVDUNG (08/06/2022)
    toggleEmployeeDialog() {
      this.isShowDialog = !this.isShowDialog;
    },

    // thực hiện việc ẩn hiện form dropDown
    // Author: HVDUNG (08/06/2022)
    toggleDropDown() {
      this.isShowDropDown = !this.isShowDropDown;
    },

    /**
     * Thực hiện reload lại dữ liệu khi click
     * Author: HVDUNG(05/06/2022)
     */
    btnRefreshOnClick() {
      if(this.textSearch==""){
        this.pagination(this.pageSize, this.pageNumberSelected, "");
      }else{
        this.pagination(this.pageSize, 1, this.textSearch);
      }
    },

     /**
     * Thực hiện xuất file excel
     * Author: HVDUNG(25/06/2022)
     */
    btnExportExcel(){
        axios
        .get("http://localhost:22454/api/v1/Employees/excel", {
          responseType: "blob",
        })
        .then((res) => {
          const url = URL.createObjectURL(new Blob([res.data]));
          const link = document.createElement("a");

          link.href = url;
          link.setAttribute("download", "Danh sách nhân viên.xlsx");

          document.body.appendChild(link);
          link.click();
        });
    },

    /**
     * Thực hiện định dạng lại ngày tháng để hiển thị
     * @param {*} dateOfBirth ngày tháng được truyền vào
     * Author: HVDUNG(05/06/2022)
     */
    formatDate(dateOfBirth) {
      if (dateOfBirth) {
        // chuyển từ dạng string sang dạng Date
        dateOfBirth = new Date(dateOfBirth);
        // lấy ngày
        let date = dateOfBirth.getDate();
        // lấy tháng
        let month = dateOfBirth.getMonth() + 1;
        // lấy năm
        let year = dateOfBirth.getFullYear();
        // thêm số 0 vào trước nếu chỉ có một kí tự
        date = date < 10 ? `0${date}` : date;
        month = month < 10 ? `0${month}` : month;
        return `${date}/${month}/${year}`;
      } else {
        return "";
      }
    },
  },

  data() {
    return {
      textSearch: "",
      employees: {},
      isShowDialog: false,
      isLoading: true,
      employeeIdSelected: null,
      employeeIdForDelete: null,
      employeeCodeForDelete: null,
      active: false,
      formDetailMode: 0,
      isShowDropDown: false,
      count: 0,
      globalTimeout: null,
      // data dùng để phân trang
      totalPages: 0,
      pageSize: 10,
      pageNumberSelected: 1
    };
  },
};
</script>

<style scoped>
@import url(../../style/css/layout/content.css);
</style>