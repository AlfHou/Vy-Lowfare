<template>
  <section>
    <b-field label="From">
      <b-autocomplete
        v-model="destination"
        placeholder="Where From?"
        :keep-first=true
        :open-on-focus=true
        :data="location"
        @select="option => selected = option"
      ></b-autocomplete>
    </b-field>
  </section>
</template>
<script>
export default {
  data() {
    return {
      destination: "",
      location: ['Oslo', 'Bergen', 'Trondheim'],
      selected: null
    };
  },
  computed: {
  }
};
</script>