<template>
  <div class="container-fluid text-center bg-info">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>

    <form @submit.prevent="updateAccount()">
      <div class="form-group">
        <label for="name">Name</label>
        <input v-model="editableAccountData.name" type="name" class="form-control" id="name" aria-describedby="name"
          placeholder="Enter Name">
        <!-- <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small> -->
      </div>
      <div class="form-group">
        <label for="picture">Picture</label>
        <input v-model="editableAccountData.picture" type="picture" class="form-control" id="picture"
          placeholder="Picture">
      </div>

      <button type="submit" class="btn btn-primary mt-2">Submit</button>
    </form>

  </div>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
export default {
  setup() {

    const editableAccountData = ref({ name: '', picture: '' })
    return {
      account: computed(() => AppState.account),
      editableAccountData,
      async updateAccount() {
        try {
          await accountService.updateAccount(editableAccountData.value)
        }
        catch (error) {
          Pop.error(error);
        }
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.container-fluid {
  /* background-color: green; */
  height: 100vh;
}
</style>
