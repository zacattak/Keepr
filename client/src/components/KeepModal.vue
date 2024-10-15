<template>
    <div class="modal fade" id="keepModal" tabindex="-1" role="dialog" aria-labelledby="keepModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div v-if="keep" class="modal-content">

                <div class="modal-body">
                    <img :src="keep.img" :alt="keep.name" class="img">
                    <h2 class="text-center">{{ keep.name }}</h2>
                    <p>{{ keep.description }}</p>

                    <p>Views:{{ keep.views }} Kept:{{ keep.kept }} </p>


                    <RouterLink class="selectable" data-dismiss="keepModal"
                        :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">

                        <img :src="keep.creator.picture" :alt="keep.creator.name" class="creatorImg">

                    </RouterLink>

                </div>
                <div class="modal-footer justify-content-between">


                    <form action="">
                        <div class="dropdown">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                                aria-expanded="false">
                                Add Keep to Vault
                            </button>

                            <ul class="dropdown-menu">
                                <div class="form-select" aria-label="Select a Vault" required>
                                    <li v-for="vault in vaults" :key="vault.id" :value="vault.id" class="dropdown-item"
                                        @click="createVaultKeep(vault)">
                                        {{ vault.name }}
                                    </li>
                                </div>
                            </ul>
                        </div>
                    </form>



                    <form @submit.prevent="handleSubmit">
                        <div class="form-group">
                            <label for="exampleTag">Create Tag</label>
                            <input v-model="editableTagData.name" type="text" class="form-control" id="exampleTag"
                                aria-describedby="tagHelp" placeholder="Tag" required maxlength="100">

                        </div>
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </form>

                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import { vaultsService } from '../services/VaultsService';
import { tagsService } from '../services/TagsService';
import Pop from '../utils/Pop';
import { Vault } from '../models/Vault';
import { Account } from '../models/Account';
import VaultListComponent from '../components/VaultListComponent.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { logger } from '../utils/Logger';
import { keepTagsService } from '../services/KeepTagsService';



export default {

    props: {
        account: { type: Account },

    },
    setup() {

        const editableVaultKeepData = ref({})

        const editableTagData = ref({ name: '' })

        const activeKeep = computed(() => AppState.activeKeep)



        async function createTag() {
            try {
                const tag = await tagsService.createTag(editableTagData.value)
                editableTagData.value = { name: '' }
                Pop.success(`${tag.name} has been created`)
                return tag;
            }
            catch (error) {
                Pop.error(error);
                throw error;
            }
        }

        async function createKeepTag(tag) {
            try {


                const keepTagData = {
                    tagId: tag.id,
                    keepId: activeKeep.value.id
                }

                await keepTagsService.createKeepTag(keepTagData)
            }
            catch (error) {
                Pop.error(error);
            }
        }

        async function handleSubmit() {
            try {
                // Call createTag and wait for it to complete

                const tag = await createTag();

                // Once createTag is successful, call createKeepTag with the tag's ID
                if (tag) {

                    await createKeepTag(tag);
                }

                Pop.success('Tag and keepTag created successfully');
            } catch (error) {
                Pop.error('Error occurred while creating tag or keepTag.');
            }
        }



        return {
            editableVaultKeepData,
            editableTagData,
            vaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
            keep: computed(() => AppState.activeKeep),
            activeKeep,
            createTag,
            createKeepTag,
            handleSubmit,
            async createVaultKeep(vault) {
                try {
                    logger.log('vault id:', vault.id)
                    logger.log('keep Id', activeKeep.value.id)



                    const vaultKeepData = {
                        vaultId: vault.id,
                        keepId: activeKeep.value.id
                    }

                    await vaultKeepsService.createVaultKeep(vaultKeepData)
                }
                catch (error) {
                    Pop.error(error);
                }
            },





        }
    },
    components: { VaultListComponent }
}
</script>


<style lang="scss" scoped>
.img {
    width: 100%;
    object-fit: cover;
    height: 40vh;
}

.creatorImg {
    width: 50%;
    border-radius: 100px;
    object-fit: cover;
    height: 20vh;
}
</style>