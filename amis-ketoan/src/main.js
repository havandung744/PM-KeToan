import { createApp } from 'vue'
import App from './App.vue'
import Paginate from "vuejs-paginate-next";

import router from './js/router'
import axios from 'axios'
import VueAxios from 'vue-axios'

createApp(App).use(router, VueAxios, axios, Paginate).mount('#app')