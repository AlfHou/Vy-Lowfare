<template>
  <section class="card">
    <div class="card-content columns">
      <div class="column">
        <b-field label="From" type="is-dark">
          <b-autocomplete
            v-model="destination"
            placeholder="Where From?"
            :keep-first="true"
            :open-on-focus="true"
            :data="location"
            @select="option => selected = option"
            size="is-large"
          ></b-autocomplete>
        </b-field>
      </div>
      <div class="column">
        <b-field label="To" type="is-dark">
          <b-autocomplete
            v-model="destination"
            placeholder="Where To?"
            :keep-first="true"
            :open-on-focus="true"
            :data="location"
            @select="option => selected = option"
            size="is-large"
          ></b-autocomplete>
        </b-field>
      </div>
    </div>
  </section>
</template>
<script>
export default {
  data() {
    return {
      destination: "",
      location: ["Oslo", "Bergen", "Trondheim"],
      selected: null
    };
  },
  computed: {}
};
</script>
<style scoped>
.card {
  height: 18em;
}
</style>