<template>
  <div class="calendar">
    <div v-for="row in 5" :key="row" class="columns">
      <div
        v-for="column in 7"
        :key="column"
        class="column"
      >
        <CalendarColumn>
          <template #date>
            {{"TEST"}}
          </template>
        </CalendarColumn>
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
  margin: 5em 0 5em 0;
  max-width: 90%;
}
</style>