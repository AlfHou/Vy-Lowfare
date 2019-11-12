<template>
  <b-field :label=label type="is-dark">
    <b-autocomplete
      v-model="destination"
      :placeholder=placeholder
      :keep-first="true"
      :open-on-focus="true"
      :data="location"
      @select="option => selected = option"
      size="is-large"
    ></b-autocomplete>
  </b-field>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      required: true
    },
    placeholder: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      destination: undefined,
      location: ["Oslo S", "Bergen", "Trondheim"],
      selected: null
    };
  },
  watch: {
    destination(dest) {
      this.$emit("destChange", dest)
    }
  }
};
</script>

<style>
</style>