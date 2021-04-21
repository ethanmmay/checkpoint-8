/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class VaultService {
  async getVaults() {
    try {
      const res = await api.get('api/vaults')
      AppState.vaults = res.data.map(k => new Vault(k))
    } catch (err) {
      logger.error('Couldnt load Vaults', err)
    }
  }

  async getVaultById(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      AppState.currentVault = new Vault(res.data)
    } catch (err) {
      logger.error('Couldnt load Vaults', err)
    }
  }

  async getPrivateVaults(accountId) {
    try {
      const res = await api.get(`api/profiles/${accountId}/privatevaults`)
      AppState.profileVaults = res.data.map(v => new Vault(v))
    } catch (err) {
      logger.error('Couldnt load Vaults', err)
    }
  }

  setActiveVault(vault) {
    try {
      AppState.activeVault = vault
    } catch (error) {
      logger.error('Cant set active vault', error)
    }
  }

  async createVault() {
    try {
      Swal.fire({
        title: 'New Vault',
        html: '<input type="text" id="title" class="swal2-input" placeholder="Enter Vault Name.. "><input type="text" id="description" class="swal2-input" placeholder="Describe the Vault.. ">',
        confirmButtonText: 'Post',
        focusConfirm: false,
        preConfirm: () => {
          const title = Swal.getPopup().querySelector('#title').value
          const description = Swal.getPopup().querySelector('#description').value
          if (!title || !description) {
            Swal.showValidationMessage('Please enter a title and description')
          }
          return { title: title, description: description }
        }
      }).then(async(result) => {
        const newVault = {
          name: result.value.title,
          description: result.value.description,
          creatorId: AppState.account.id
        }
        Swal.fire({
          title: 'Is this vault private?',
          input: 'checkbox',
          inputPlaceholder: 'Yes, its private'
        }).then(async function(result) {
          if (result.value) {
            Swal.fire({ icon: 'success', text: 'Created a private vault' })
            newVault.isPrivate = true
            await api.post('api/vaults', newVault)
            await profileService.getProfileVaults(AppState.account.id)
          } else if (result.value === 0) {
            Swal.fire({ icon: 'success', text: 'Created a public vault' })
            newVault.isPrivate = false
            await api.post('api/vaults', newVault)
            await profileService.getProfileVaults(AppState.account.id)
          } else {
            logger.log(`modal was dismissed by ${result.dismiss}`)
          }
        })
      })
    } catch (err) {
      logger.error('Couldnt create Vault', err)
    }
  }

  async deleteVault(vaultId) {
    try {
      if (window.confirm('Are you sure you want to delete your Vault?')) {
        await api.delete('api/vaults/' + vaultId)
        profileService.getProfileVaults(AppState.account.id)
        document.getElementById('vaultModalClose').click()
      }
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultService = new VaultService()
