<template>
  <div class="calendar">
    <div v-for="index in 5" :key="index" class="columns">
      <div
        v-for="index in 7"
        :key="index"
        class="column"
      >
        <CalendarColumn />
      </div>
    </div>
  </div>
</template>
<script>
import CalendarColumn from "./CalendarColumn";

export default {
  components: {
    CalendarColumn
  }
};
</script>
<style scoped>
.column {
  padding: 0;
  margin: 0.5rem;
}
.calendar {
  margin-top: 200px;
  max-width: 90%;
}
</style>