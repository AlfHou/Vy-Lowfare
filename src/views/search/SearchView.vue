<template>
  <section class="card">
    <div class="card-content columns">
      <div class="column">
        <Field label="From" placeholder="Where From?" />
        <b-field label="Select datetime">
          <b-datepicker placeholder="Type or select a date..." icon="calendar-today" editable></b-datepicker>
        </b-field>
      </div>
      <div class="column">
        <Field label="To" placeholder="Where to?" />
      </div>
    </div>
  </section>
</template>
<script>
import Field from "./Field";

export default {
  components: {
    Field
  }
};
</script>
<style scoped>
.card {
  height: 18em;
}
</style>