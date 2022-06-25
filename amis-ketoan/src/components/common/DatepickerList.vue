<template>
   <datepicker
    v-model="selected"
    :locale="locale"
  />
</template>

<script>
import Datepicker from 'vue3-datepicker'
export default {
     components: {
    Datepicker: Datepicker
  },
}
</script>

<style>

</style>